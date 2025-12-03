/**
 * 混合讀寫效能測試 (Mixed Read/Write Performance Test)
 *
 * 依據實際流量模型設計的混合場景:
 * - 主要讀取 (60–70%): 商品列表 / 輕量查詢 / 首頁推薦
 * - 次要讀取 (15–25%): 商品詳情 / 訂單列表 / 訂單詳情
 * - 一般寫入 (5–15%): 更新購物車、用戶註冊/登入等
 * - 重度寫入 (< 5%): 結帳 (建立訂單)
 *
 * 預設比例 (可透過環境變數調整):
 *   MAIN_READ_RATIO      = 0.65  // 主要讀取
 *   SECONDARY_READ_RATIO = 0.20  // 次要讀取
 *   WRITE_RATIO          = 0.10  // 一般寫入
 *   HEAVY_WRITE_RATIO    = 0.05  // 重度寫入
 *
 * 執行方式:
 *   k6 run k6/scripts/mixed-traffic-test.js
 *
 * 自定義參數範例:
 *   BASE_URL=http://localhost:5025 MAX_VUS=300 DURATION=2m k6 run k6/scripts/mixed-traffic-test.js
 *   MAIN_READ_RATIO=0.6 SECONDARY_READ_RATIO=0.25 WRITE_RATIO=0.1 HEAVY_WRITE_RATIO=0.05 k6 run k6/scripts/mixed-traffic-test.js
 */

import http from 'k6/http';
import { check } from 'k6';
import { Rate, Trend } from 'k6/metrics';
import {
    getBaseUrl,
    getAuthHeaders,
    parseApiResponse,
    randomSleep,
    generateSignUpData,
    generateCartData,
    generateOrderData,
} from '../utils/helpers.js';

// ===== 指標定義 =====
const errorRate = new Rate('errors');
const realErrorRate = new Rate('real_errors');
const responseTimeTrend = new Trend('response_time_trend');

// 各類型流量的響應時間
const mainReadTime = new Trend('main_read_time');
const secondaryReadTime = new Trend('secondary_read_time');
const writeTime = new Trend('write_time');
const heavyWriteTime = new Trend('heavy_write_time');

// 各類型流量的比例統計
const mainReadHit = new Rate('main_read_hit');
const secondaryReadHit = new Rate('secondary_read_hit');
const writeHit = new Rate('write_hit');
const heavyWriteHit = new Rate('heavy_write_hit');

// ===== 共用工具 =====
const BASE_URL = getBaseUrl();
const MAX_VUS = parseInt(__ENV.MAX_VUS) || 300;
const DURATION = __ENV.DURATION || '2m';
const TEST_USERNAME = __ENV.TEST_USERNAME || 'testuser';
const TEST_PASSWORD = __ENV.TEST_PASSWORD || 'Test123456!';

// 預設比例（可由環境變數覆蓋）
const MAIN_READ_RATIO = __ENV.MAIN_READ_RATIO ? parseFloat(__ENV.MAIN_READ_RATIO) : 0.65;
const SECONDARY_READ_RATIO = __ENV.SECONDARY_READ_RATIO ? parseFloat(__ENV.SECONDARY_READ_RATIO) : 0.20;
const WRITE_RATIO = __ENV.WRITE_RATIO ? parseFloat(__ENV.WRITE_RATIO) : 0.10;
const HEAVY_WRITE_RATIO = __ENV.HEAVY_WRITE_RATIO ? parseFloat(__ENV.HEAVY_WRITE_RATIO) : 0.05;

/**
 * 解析時間字串成秒數 (例如 "2m", "30s")
 */
function parseDuration(durationStr) {
    let totalSeconds = 0;
    const regex = /(\d+)([smh])/g;
    let match;

    while ((match = regex.exec(durationStr)) !== null) {
        const value = parseInt(match[1]);
        const unit = match[2];

        switch (unit) {
            case 's':
                totalSeconds += value;
                break;
            case 'm':
                totalSeconds += value * 60;
                break;
            case 'h':
                totalSeconds += value * 3600;
                break;
        }
    }

    return totalSeconds || 30;
}

// 動態計算 stages（總和 = 100%）
const totalSeconds = parseDuration(DURATION);
const rampUp1 = Math.max(3, Math.floor(totalSeconds * 0.1));   // 10%
const rampUp2 = Math.max(3, Math.floor(totalSeconds * 0.2));  // 20%
const rampUp3 = Math.max(3, Math.floor(totalSeconds * 0.2));   // 20%
const holdTime = Math.max(5, Math.floor(totalSeconds * 0.4));  // 40%
const rampDown = Math.max(3, Math.floor(totalSeconds * 0.1));  // 10%

export const options = {
    stages: [
        { duration: `${rampUp1}s`, target: 10 },
        { duration: `${rampUp2}s`, target: 50 },
        { duration: `${rampUp3}s`, target: MAX_VUS },
        { duration: `${holdTime}s`, target: MAX_VUS },
        { duration: `${rampDown}s`, target: 0 },
    ],
    thresholds: {
        http_req_duration: ['p(50)<1500', 'p(95)<5000', 'p(99)<8000'],
        http_req_failed: ['rate<0.15'],
        errors: ['rate<0.10'],
        real_errors: ['rate<0.05'],
        main_read_time: ['p(95)<3000'],
        secondary_read_time: ['p(95)<4000'],
        write_time: ['p(95)<6000'],
        heavy_write_time: ['p(95)<8000'],
    },
    summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(50)', 'p(75)', 'p(90)', 'p(95)', 'p(99)'],
};

// 在混合測試中可接受的 HTTP 狀態碼 (讀 + 寫的聯集)
const ACCEPTABLE_STATUS_CODES = [200, 400, 401, 403, 404, 409, 429, 500, 503];

function isRealError(statusCode) {
    return !ACCEPTABLE_STATUS_CODES.includes(statusCode);
}

// ===== 認證 (與 read-only-test 類似：共用一個測試帳號) =====
export function setup() {
    const loginData = {
        Username: TEST_USERNAME,
        Password: TEST_PASSWORD,
    };

    const loginResponse = http.post(`${BASE_URL}/User/UserLogin`, JSON.stringify(loginData), {
        headers: getAuthHeaders(),
    });

    if (loginResponse.status === 200) {
        console.log('✓ 混合測試預登入成功');
        return { authenticated: true };
    } else {
        console.log('⚠ 混合測試預登入失敗，部分需要認證的 API 可能回傳 401');
        return { authenticated: false };
    }
}

// ===== 各類型場景實作 =====

// 主要讀取：商品列表 / 輕量查詢 / 首頁推薦
function executeMainRead() {
    // 以輕量商品列表為主 (最接近首頁 / 推薦流量)
    const res = http.get(
        `${BASE_URL}/Product/GetProductBasicInfoList?tag=all&kind=all&query=`,
        {
            headers: getAuthHeaders(),
            tags: { name: 'GetProductBasicInfoList', traffic_type: 'main_read' },
        }
    );

    const ok = check(res, {
        '主要讀取 HTTP 可處理': (r) => [200, 404, 503].includes(r.status),
    });

    const realErr = !ok && isRealError(res.status);
    errorRate.add(realErr ? 1 : 0);
    realErrorRate.add(isRealError(res.status) ? 1 : 0);
    responseTimeTrend.add(res.timings.duration);
    mainReadTime.add(res.timings.duration);
    mainReadHit.add(1);

    randomSleep(0.2, 0.8);
}

// 次要讀取：商品詳情 / 訂單列表 / 訂單詳情
function executeSecondaryRead() {
    const choice = Math.random();
    let res;

    if (choice < 0.4) {
        // 商品詳情
        const productId = Math.floor(Math.random() * 100) + 1;
        res = http.get(
            `${BASE_URL}/Product/GetProductById?productId=${productId}`,
            {
                headers: getAuthHeaders(),
                tags: { name: 'GetProductById', traffic_type: 'secondary_read' },
            }
        );
    } else if (choice < 0.8) {
        // 訂單列表
        res = http.get(
            `${BASE_URL}/Order/GetOrders?query=`,
            {
                headers: getAuthHeaders(),
                tags: { name: 'GetOrders', traffic_type: 'secondary_read' },
            }
        );
    } else {
        // 訂單詳情 (隨機訂單編號，重點在查詢索引效能)
        const orderCode = `EC${Math.random().toString(36).substring(2, 12).toUpperCase()}`;
        res = http.get(
            `${BASE_URL}/Order/GetOrderInfo?recordCode=${orderCode}`,
            {
                headers: getAuthHeaders(),
                tags: { name: 'GetOrderInfo', traffic_type: 'secondary_read' },
            }
        );
    }

    const ok = check(res, {
        '次要讀取 HTTP 可處理': (r) => [200, 401, 404, 429, 503].includes(r.status),
    });

    const realErr = !ok && isRealError(res.status);
    errorRate.add(realErr ? 1 : 0);
    realErrorRate.add(isRealError(res.status) ? 1 : 0);
    responseTimeTrend.add(res.timings.duration);
    secondaryReadTime.add(res.timings.duration);
    secondaryReadHit.add(1);

    randomSleep(0.3, 1.0);
}

// 一般寫入：更新購物車 / 用戶註冊 / 登入 等較輕寫入
function executeWrite() {
    const choice = Math.random();

    // 大部分流量走「更新購物車」，少部分是註冊 / 登入
    if (choice < 0.7) {
        // 更新購物車
        const cartData = generateCartData();
        const res = http.post(
            `${BASE_URL}/Cart/MergeCartContent`,
            JSON.stringify(cartData),
            {
                headers: getAuthHeaders(),
                tags: { name: 'MergeCartContent', traffic_type: 'write' },
            }
        );

        const ok = check(res, {
            '購物車寫入 HTTP 可處理': (r) => [200, 400, 401, 429, 500, 503].includes(r.status),
        });

        const realErr = !ok && isRealError(res.status);
        errorRate.add(realErr ? 1 : 0);
        realErrorRate.add(isRealError(res.status) ? 1 : 0);
        responseTimeTrend.add(res.timings.duration);
        writeTime.add(res.timings.duration);
    } else if (choice < 0.85) {
        // 用戶註冊
        const userData = generateSignUpData();
        const res = http.post(
            `${BASE_URL}/User/UserRegister`,
            JSON.stringify(userData),
            {
                headers: getAuthHeaders(),
                tags: { name: 'UserRegister', traffic_type: 'write' },
            }
        );

        const ok = check(res, {
            '註冊寫入 HTTP 可處理': (r) => [200, 400, 409, 429, 500, 503].includes(r.status),
        });

        const realErr = !ok && isRealError(res.status);
        errorRate.add(realErr ? 1 : 0);
        realErrorRate.add(isRealError(res.status) ? 1 : 0);
        responseTimeTrend.add(res.timings.duration);
        writeTime.add(res.timings.duration);
    } else {
        // 用戶登入 (session / token 寫入)
        const loginData = {
            Username: TEST_USERNAME,
            Password: TEST_PASSWORD,
        };
        const res = http.post(
            `${BASE_URL}/User/UserLogin`,
            JSON.stringify(loginData),
            {
                headers: getAuthHeaders(),
                tags: { name: 'UserLogin', traffic_type: 'write' },
            }
        );

        const ok = check(res, {
            '登入寫入 HTTP 可處理': (r) => [200, 401, 429, 500, 503].includes(r.status),
        });

        const realErr = !ok && isRealError(res.status);
        errorRate.add(realErr ? 1 : 0);
        realErrorRate.add(isRealError(res.status) ? 1 : 0);
        responseTimeTrend.add(res.timings.duration);
        writeTime.add(res.timings.duration);
    }

    writeHit.add(1);
    randomSleep(0.5, 1.5);
}

// 重度寫入：結帳 (建立訂單)
function executeHeavyWrite() {
    const orderData = generateOrderData();
    const res = http.post(
        `${BASE_URL}/Order/SubmitOrder`,
        JSON.stringify(orderData),
        {
            headers: getAuthHeaders(),
            tags: { name: 'SubmitOrder', traffic_type: 'heavy_write' },
        }
    );

    const ok = check(res, {
        '結帳 / 建立訂單 HTTP 可處理': (r) => [200, 400, 401, 403, 429, 500, 503].includes(r.status),
    });

    const realErr = !ok && isRealError(res.status);
    errorRate.add(realErr ? 1 : 0);
    realErrorRate.add(isRealError(res.status) ? 1 : 0);
    responseTimeTrend.add(res.timings.duration);
    heavyWriteTime.add(res.timings.duration);
    heavyWriteHit.add(1);

    // 重度寫入之間睡久一點，貼近真實結帳行為
    randomSleep(1, 3);
}

// ===== 主流程：依比例隨機選擇一種場景執行 =====
export default function () {
    const r = Math.random();
    const rMain = MAIN_READ_RATIO;
    const rSec = rMain + SECONDARY_READ_RATIO;
    const rWrite = rSec + WRITE_RATIO;

    if (r < rMain) {
        executeMainRead();
    } else if (r < rSec) {
        executeSecondaryRead();
    } else if (r < rWrite) {
        executeWrite();
    } else {
        executeHeavyWrite();
    }
}


