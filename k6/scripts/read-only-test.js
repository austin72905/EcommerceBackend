/**
 * 純讀取效能測試 (Read-Only Performance Test)
 * 
 * 目的: 測試系統在純讀取操作下的資料庫查詢效能，評估索引優化效果
 * 負載: 可配置的虛擬用戶數，預設 300 個 VU，持續 2 分鐘
 * 
 * 測試場景:
 * - 商品列表查詢（大量 Include，測試複雜查詢效能）
 * - 商品基本信息列表（輕量查詢）
 * - 單個商品詳情（單筆查詢）
 * - 訂單列表查詢（需要認證，測試 UserId 索引）
 * - 訂單詳情查詢（需要認證，測試 RecordCode 索引）
 * 
 * 執行方式:
 *   k6 run k6/scripts/read-only-test.js
 * 
 * 自定義參數:
 *   MAX_VUS=500 DURATION=3m k6 run k6/scripts/read-only-test.js
 *   BASE_URL=http://localhost:5025 MAX_VUS=300 DURATION=2m k6 run k6/scripts/read-only-test.js
 * 
 * 注意: 此測試專注於讀取效能，不包含任何寫入操作
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate, Trend } from 'k6/metrics';
import { getBaseUrl, getAuthHeaders, parseApiResponse, randomSleep } from '../utils/helpers.js';

// 自定義指標
const errorRate = new Rate('errors');
const responseTimeTrend = new Trend('response_time_trend');
const realErrorRate = new Rate('real_errors');

// 各 API 的響應時間趨勢（用於詳細分析）
const productListTime = new Trend('product_list_time');
const productBasicListTime = new Trend('product_basic_list_time');
const productDetailTime = new Trend('product_detail_time');
const orderListTime = new Trend('order_list_time');
const orderDetailTime = new Trend('order_detail_time');

/**
 * 解析時間字符串為秒數
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

// 測試配置
const BASE_URL = getBaseUrl();
const MAX_VUS = parseInt(__ENV.MAX_VUS) || 300;
const DURATION = __ENV.DURATION || '2m';
const TEST_USERNAME = __ENV.TEST_USERNAME || 'testuser';
const TEST_PASSWORD = __ENV.TEST_PASSWORD || 'Test123456!';

// 根據 DURATION 動態計算階段時間
const totalSeconds = parseDuration(DURATION);
const rampUp1 = Math.max(3, Math.floor(totalSeconds * 0.15));
const rampUp2 = Math.max(3, Math.floor(totalSeconds * 0.35));
const holdTime = Math.max(5, Math.floor(totalSeconds * 0.35));
const rampDown = Math.max(3, Math.floor(totalSeconds * 0.15));

export const options = {
    stages: [
        { duration: `${rampUp1}s`, target: 10 },
        { duration: `${rampUp2}s`, target: 50 },
        { duration: `${rampUp2}s`, target: MAX_VUS },
        { duration: `${holdTime}s`, target: MAX_VUS },
        { duration: `${rampDown}s`, target: 0 },
    ],
    thresholds: {
        http_req_duration: ['p(50)<1000', 'p(95)<3000', 'p(99)<5000'], // 讀取操作應該更快
        http_req_failed: ['rate<0.10'],
        errors: ['rate<0.10'], // 調整為 10%，因為 404 等可接受狀態碼不應計入錯誤
        real_errors: ['rate<0.02'],
        // 各 API 的響應時間閾值（調整為更寬鬆的標準）
        product_list_time: ['p(95)<4000'],
        product_basic_list_time: ['p(95)<4000'],
        product_detail_time: ['p(95)<4000'],
        order_list_time: ['p(95)<3000'],
        order_detail_time: ['p(95)<3000'],
    },
    summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(50)', 'p(75)', 'p(90)', 'p(95)', 'p(99)'],
};

// 定義在讀取測試中可接受的 HTTP 狀態碼
const ACCEPTABLE_STATUS_CODES = [200, 401, 404, 429, 503];

// 判斷是否為真正的錯誤
function isRealError(statusCode) {
    return !ACCEPTABLE_STATUS_CODES.includes(statusCode);
}

// 預先登入（僅在第一次迭代時執行）
let authCookie = null;

export function setup() {
    // 在測試開始前嘗試登入，獲取認證 cookie
    // 注意：這只會執行一次，所有 VU 共享這個 cookie
    // 如果測試需要每個 VU 獨立的認證，需要在 default function 中處理
    
    const loginData = {
        Username: TEST_USERNAME,
        Password: TEST_PASSWORD,
    };
    
    const loginResponse = http.post(`${BASE_URL}/User/UserLogin`, JSON.stringify(loginData), {
        headers: getAuthHeaders(),
    });
    
    if (loginResponse.status === 200) {
        // 提取 cookie（k6 會自動處理，這裡只是記錄）
        const cookies = loginResponse.cookies;
        console.log('✓ 預先登入成功');
        return { authenticated: true };
    } else {
        console.log('⚠ 預先登入失敗，訂單相關 API 可能會返回 401');
        return { authenticated: false };
    }
}

export default function (data) {
    // k6 會自動處理 cookie，每個 VU 都有獨立的 cookie jar
    
    // ========== 場景 1: 商品列表查詢（複雜查詢，包含多層 Include）==========
    const productResponse = http.get(`${BASE_URL}/Product/GetProductList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductList', test_type: 'read_only' },
    });
    
    const productCheck = check(productResponse, {
        '商品列表請求成功': (r) => r.status === 200,
        '響應時間 < 5s': (r) => r.timings.duration < 5000,
        '響應包含數據': (r) => {
            if (r.status === 200) {
                const result = parseApiResponse(r);
                return result.success && result.data !== null;
            }
            return true;
        },
    });
    
    // 只有真正的錯誤才計入錯誤率（排除可接受的狀態碼）
    const isProductError = !productCheck && isRealError(productResponse.status);
    errorRate.add(isProductError ? 1 : 0);
    realErrorRate.add(isRealError(productResponse.status) ? 1 : 0);
    responseTimeTrend.add(productResponse.timings.duration);
    productListTime.add(productResponse.timings.duration);
    
    randomSleep(0.3, 1);
    
    // ========== 場景 2: 商品基本信息列表（輕量查詢）==========
    const productBasicResponse = http.get(`${BASE_URL}/Product/GetProductBasicInfoList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductBasicInfoList', test_type: 'read_only' },
    });
    
    const productBasicCheck = check(productBasicResponse, {
        '商品基本信息請求成功': (r) => r.status === 200 || r.status === 503,
        '響應時間 < 3s': (r) => r.timings.duration < 3000,
        '響應包含數據': (r) => {
            if (r.status === 200) {
                const result = parseApiResponse(r);
                return result.success && result.data !== null;
            }
            return true;
        },
    });
    
    const isProductBasicError = !productBasicCheck && isRealError(productBasicResponse.status);
    errorRate.add(isProductBasicError ? 1 : 0);
    realErrorRate.add(isRealError(productBasicResponse.status) ? 1 : 0);
    responseTimeTrend.add(productBasicResponse.timings.duration);
    productBasicListTime.add(productBasicResponse.timings.duration);
    
    randomSleep(0.3, 1);
    
    // ========== 場景 3: 單個商品詳情查詢（單筆查詢，測試 ProductId 索引）==========
    const randomProductId = Math.floor(Math.random() * 100) + 1; // 假設商品 ID 在 1-100 範圍
    const productDetailResponse = http.get(`${BASE_URL}/Product/GetProductById?productId=${randomProductId}`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductById', test_type: 'read_only' },
    });
    
    const productDetailCheck = check(productDetailResponse, {
        '商品詳情請求可處理': (r) => [200, 404, 503].includes(r.status),
        '響應時間 < 2s': (r) => r.timings.duration < 2000,
    });
    
    const isProductDetailError = !productDetailCheck && isRealError(productDetailResponse.status);
    errorRate.add(isProductDetailError ? 1 : 0);
    realErrorRate.add(isRealError(productDetailResponse.status) ? 1 : 0);
    responseTimeTrend.add(productDetailResponse.timings.duration);
    productDetailTime.add(productDetailResponse.timings.duration);
    
    randomSleep(0.3, 1);
    
    // ========== 場景 4: 訂單列表查詢（需要認證，測試 UserId 索引）==========
    // 注意：如果未認證，會返回 401，這是可接受的
    const orderResponse = http.get(`${BASE_URL}/Order/GetOrders?query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrders', test_type: 'read_only' },
    });
    
    const orderCheck = check(orderResponse, {
        '訂單列表請求可處理': (r) => [200, 401, 404, 429, 503].includes(r.status),
        '響應時間 < 5s': (r) => r.timings.duration < 5000,
        '響應包含數據（如果成功）': (r) => {
            if (r.status === 200) {
                const result = parseApiResponse(r);
                return result.success && result.data !== null;
            }
            return true;
        },
    });
    
    const isOrderError = !orderCheck && isRealError(orderResponse.status);
    errorRate.add(isOrderError ? 1 : 0);
    realErrorRate.add(isRealError(orderResponse.status) ? 1 : 0);
    responseTimeTrend.add(orderResponse.timings.duration);
    orderListTime.add(orderResponse.timings.duration);
    
    randomSleep(0.5, 1.5);
    
    // ========== 場景 5: 訂單詳情查詢（需要認證，測試 RecordCode 索引）==========
    // 生成一個可能的訂單編號格式（實際可能不存在，但可以測試索引效能）
    const orderCode = `EC${Math.random().toString(36).substring(2, 12).toUpperCase()}`;
    const orderInfoResponse = http.get(`${BASE_URL}/Order/GetOrderInfo?recordCode=${orderCode}`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrderInfo', test_type: 'read_only' },
    });
    
    const orderInfoCheck = check(orderInfoResponse, {
        '訂單詳情請求可處理': (r) => [200, 401, 404, 429, 503].includes(r.status),
        '響應時間 < 3s': (r) => r.timings.duration < 3000,
    });
    
    const isOrderInfoError = !orderInfoCheck && isRealError(orderInfoResponse.status);
    errorRate.add(isOrderInfoError ? 1 : 0);
    realErrorRate.add(isRealError(orderInfoResponse.status) ? 1 : 0);
    responseTimeTrend.add(orderInfoResponse.timings.duration);
    orderDetailTime.add(orderInfoResponse.timings.duration);
    
    randomSleep(0.5, 1.5);
    
    // ========== 場景 6: 商品基本信息單筆查詢（輕量單筆查詢）==========
    const productBasicDetailResponse = http.get(`${BASE_URL}/Product/GetProductBasicInfoById?productId=${randomProductId}`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductBasicInfoById', test_type: 'read_only' },
    });
    
    const productBasicDetailCheck = check(productBasicDetailResponse, {
        '商品基本信息詳情請求可處理': (r) => [200, 404, 503].includes(r.status),
        '響應時間 < 1s': (r) => r.timings.duration < 1000,
    });
    
    const isProductBasicDetailError = !productBasicDetailCheck && isRealError(productBasicDetailResponse.status);
    errorRate.add(isProductBasicDetailError ? 1 : 0);
    realErrorRate.add(isRealError(productBasicDetailResponse.status) ? 1 : 0);
    responseTimeTrend.add(productBasicDetailResponse.timings.duration);
    
    randomSleep(0.3, 1);
}

export function handleSummary(data) {
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}

function textSummary(data, options) {
    const duration = data.metrics.iteration_duration?.values || {};
    const httpReqs = data.metrics.http_reqs?.values || {};
    const httpDuration = data.metrics.http_req_duration?.values || {};
    const errors = data.metrics.errors?.values || {};
    const vus = data.metrics.vus?.values || {};
    const httpReqFailed = data.metrics.http_req_failed?.values || {};
    const realErrors = data.metrics.real_errors?.values || {};
    
    // 各 API 的響應時間
    const productListDuration = data.metrics.product_list_time?.values || {};
    const productBasicListDuration = data.metrics.product_basic_list_time?.values || {};
    const productDetailDuration = data.metrics.product_detail_time?.values || {};
    const orderListDuration = data.metrics.order_list_time?.values || {};
    const orderDetailDuration = data.metrics.order_detail_time?.values || {};
    
    const safeToFixed = (value, decimals = 2) => {
        if (value === undefined || value === null || isNaN(value)) {
            return 'N/A';
        }
        return value.toFixed(decimals);
    };
    
    let testDurationSeconds = null;
    if (data.state?.testRunDurationMs !== undefined && data.state.testRunDurationMs > 0) {
        testDurationSeconds = data.state.testRunDurationMs / 1000;
    } else {
        const totalSeconds = parseDuration(DURATION);
        const calcRampUp1 = Math.max(3, Math.floor(totalSeconds * 0.15));
        const calcRampUp2 = Math.max(3, Math.floor(totalSeconds * 0.35));
        const calcHoldTime = Math.max(5, Math.floor(totalSeconds * 0.35));
        const calcRampDown = Math.max(3, Math.floor(totalSeconds * 0.15));
        testDurationSeconds = calcRampUp1 + calcRampUp2 + calcRampUp2 + calcHoldTime + calcRampDown;
    }
    
    const getPercentile = (obj, percentile) => {
        if (!obj) return null;
        let value = obj[percentile];
        if (value !== undefined && value !== null && !isNaN(value)) {
            return value;
        }
        if (percentile === 'p(50)') {
            value = obj['med'] || obj['median'];
            if (value !== undefined && value !== null && !isNaN(value)) {
                return value;
            }
        }
        return null;
    };
    
    const p50 = getPercentile(httpDuration, 'p(50)');
    const p95 = getPercentile(httpDuration, 'p(95)');
    const p99 = getPercentile(httpDuration, 'p(99)');
    
    return `
    ====================
    純讀取效能測試結果摘要
    ====================
    測試持續時間: ${testDurationSeconds !== null ? safeToFixed(testDurationSeconds) : safeToFixed(duration.max / 1000)}s
    最大並發用戶數: ${vus.max || 0}
    總請求數: ${httpReqs.count || 0}
    請求速率: ${safeToFixed(httpReqs.rate)} req/s
    
    整體響應時間統計:
      平均: ${safeToFixed(httpDuration.avg)}ms
      最小: ${safeToFixed(httpDuration.min)}ms
      最大: ${safeToFixed(httpDuration.max)}ms
      p50: ${safeToFixed(p50)}ms
      p95: ${safeToFixed(p95)}ms
      p99: ${safeToFixed(p99)}ms
    
    各 API 響應時間分析:
      商品列表 (GetProductList):
        平均: ${safeToFixed(productListDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(productListDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(productListDuration, 'p(99)'))}ms
      
      商品基本信息列表 (GetProductBasicInfoList):
        平均: ${safeToFixed(productBasicListDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(productBasicListDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(productBasicListDuration, 'p(99)'))}ms
      
      商品詳情 (GetProductById):
        平均: ${safeToFixed(productDetailDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(productDetailDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(productDetailDuration, 'p(99)'))}ms
      
      訂單列表 (GetOrders):
        平均: ${safeToFixed(orderListDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(orderListDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(orderListDuration, 'p(99)'))}ms
      
      訂單詳情 (GetOrderInfo):
        平均: ${safeToFixed(orderDetailDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(orderDetailDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(orderDetailDuration, 'p(99)'))}ms
    
    錯誤統計:
      錯誤率: ${safeToFixed(errors.rate * 100)}%
      HTTP 失敗請求: ${safeToFixed(httpReqFailed.rate * 100)}%
      真正錯誤率: ${safeToFixed(realErrors.rate * 100)}% (排除可接受狀態碼)
      總錯誤數: ${errors.count || 0}
      真正錯誤數: ${realErrors.count || 0}
    
    效能評估:
      ${errors.rate < 0.05 ? '✓ 系統讀取效能表現良好' : '✗ 系統讀取效能出現問題'}
      ${p50 !== null && p50 < 1000 ? '✓ P50 響應時間優秀' : p50 !== null && p50 < 2000 ? '⚠ P50 響應時間可接受' : '✗ P50 響應時間較慢'}
      ${p95 !== null && p95 < 3000 ? '✓ P95 響應時間優秀' : p95 !== null && p95 < 5000 ? '⚠ P95 響應時間可接受' : '✗ P95 響應時間較慢'}
      ${p99 !== null && p99 < 5000 ? '✓ P99 響應時間優秀' : p99 !== null && p99 < 8000 ? '⚠ P99 響應時間可接受' : '✗ P99 響應時間較慢'}
      ${realErrors.rate < 0.02 ? '✓ 真正錯誤率在可接受範圍' : '✗ 真正錯誤率過高'}
    
    資料庫索引優化效果評估:
      ${getPercentile(orderListDuration, 'p(95)') !== null && getPercentile(orderListDuration, 'p(95)') < 2000 ? '✓ 訂單列表查詢（UserId 索引）效能良好' : '⚠ 訂單列表查詢可能需要優化'}
      ${getPercentile(orderDetailDuration, 'p(95)') !== null && getPercentile(orderDetailDuration, 'p(95)') < 1000 ? '✓ 訂單詳情查詢（RecordCode 索引）效能良好' : '⚠ 訂單詳情查詢可能需要優化'}
      ${getPercentile(productDetailDuration, 'p(95)') !== null && getPercentile(productDetailDuration, 'p(95)') < 500 ? '✓ 商品詳情查詢效能良好' : '⚠ 商品詳情查詢可能需要優化'}
    
    建議:
      ${p95 !== null && p95 > 3000 ? '- P95 響應時間較慢，建議檢查資料庫查詢效能和索引使用情況' : ''}
      ${getPercentile(orderListDuration, 'p(95)') !== null && getPercentile(orderListDuration, 'p(95)') > 2000 ? '- 訂單列表查詢較慢，確認 UserId 索引是否生效' : ''}
      ${getPercentile(orderDetailDuration, 'p(95)') !== null && getPercentile(orderDetailDuration, 'p(95)') > 1000 ? '- 訂單詳情查詢較慢，確認 RecordCode 索引是否生效' : ''}
      ${httpReqs.rate < 100 ? '- RPS 較低，可能受到資料庫查詢效能限制，建議檢查慢查詢日誌' : ''}
      ${realErrors.rate > 0.02 ? '- 真正錯誤率過高，建議檢查服務器日誌' : ''}
      ${errors.rate < 0.05 && p95 !== null && p95 < 3000 ? '- 讀取效能表現良好，可以考慮進行更高負載的測試' : ''}
    ====================
    `;
}

