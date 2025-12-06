/**
 * 混合讀寫效能測試（含支付回調） (Mixed Read/Write Performance Test with Payment Callback)
 *
 * 基於 mixed-traffic-test.js，額外增加支付成功回調測試
 * - 在 SubmitOrder 成功後，直接調用 ECPayReturn 模擬支付成功
 * - 測試支付成功後的完整流程（MQ 消息發送等）
 * - 不需要調用 ec-payment-service
 *
 * 預設比例 (可透過環境變數調整):
 *   MAIN_READ_RATIO      = 0.65  // 主要讀取
 *   SECONDARY_READ_RATIO = 0.20  // 次要讀取
 *   WRITE_RATIO          = 0.10  // 一般寫入
 *   HEAVY_WRITE_RATIO    = 0.05  // 重度寫入（含支付回調）
 *
 * 執行方式:
 *   k6 run k6/scripts/mixed-traffic-with-payment-test.js
 *
 * 自定義參數範例:
 *   BASE_URL=http://localhost:5025 MAX_VUS=300 DURATION=2m k6 run k6/scripts/mixed-traffic-with-payment-test.js
 *   PAYMENT_HASH_KEY=your_key PAYMENT_HASH_IV=your_iv k6 run k6/scripts/mixed-traffic-with-payment-test.js
 */

import http from 'k6/http';
import { check } from 'k6';
import { Rate, Trend } from 'k6/metrics';
import crypto from 'k6/crypto';
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
const paymentCallbackTime = new Trend('payment_callback_time');

// 各類型流量的比例統計
const mainReadHit = new Rate('main_read_hit');
const secondaryReadHit = new Rate('secondary_read_hit');
const writeHit = new Rate('write_hit');
const heavyWriteHit = new Rate('heavy_write_hit');
const paymentCallbackHit = new Rate('payment_callback_hit');

// ===== 共用工具 =====
const BASE_URL = getBaseUrl();
const MAX_VUS = parseInt(__ENV.MAX_VUS) || 300;
const DURATION = __ENV.DURATION || '2m';
const TEST_PASSWORD = __ENV.TEST_PASSWORD || 'Test123456!';

// ===== 固定測試用戶列表（100個用戶）=====
const TEST_USERS = [];
for (let i = 1; i <= 100; i++) {
    TEST_USERS.push({
        Username: `testuser${i}`,
        Password: TEST_PASSWORD,
        Email: `testuser${i}@test.com`,
    });
}

/**
 * 根據當前 VU ID 獲取對應的測試用戶
 * __VU 是 k6 的內建變數，表示當前 VU 的 ID（從 1 開始）
 * 使用模運算來循環使用100個用戶
 */
function getCurrentUser() {
    const vuIndex = (__VU - 1) % TEST_USERS.length;
    return TEST_USERS[vuIndex];
}

// 預設比例（可由環境變數覆蓋）
const MAIN_READ_RATIO = __ENV.MAIN_READ_RATIO ? parseFloat(__ENV.MAIN_READ_RATIO) : 0.65;
const SECONDARY_READ_RATIO = __ENV.SECONDARY_READ_RATIO ? parseFloat(__ENV.SECONDARY_READ_RATIO) : 0.20;
const WRITE_RATIO = __ENV.WRITE_RATIO ? parseFloat(__ENV.WRITE_RATIO) : 0.10;
const HEAVY_WRITE_RATIO = __ENV.HEAVY_WRITE_RATIO ? parseFloat(__ENV.HEAVY_WRITE_RATIO) : 0.05;

// 支付回調相關配置（預設值，應與後端配置一致）
const PAYMENT_HASH_KEY = __ENV.PAYMENT_HASH_KEY || 'pwFHCqoQZGmho4w6';
const PAYMENT_HASH_IV = __ENV.PAYMENT_HASH_IV || 'EkRm7iFT261dpevs';
const PAYMENT_MERCHANT_ID = __ENV.PAYMENT_MERCHANT_ID || '3002607';

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
        payment_callback_time: ['p(95)<5000'],
    },
    summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(50)', 'p(75)', 'p(90)', 'p(95)', 'p(99)'],
};

// 在混合測試中可接受的 HTTP 狀態碼 (讀 + 寫的聯集)
const ACCEPTABLE_STATUS_CODES = [200, 400, 401, 403, 404, 409, 429, 500, 503];

function isRealError(statusCode) {
    return !ACCEPTABLE_STATUS_CODES.includes(statusCode);
}

/**
 * 模擬 HttpUtility.UrlEncode 的行為
 * HttpUtility.UrlEncode 會將空格編碼為 +，而 encodeURIComponent 會編碼為 %20
 * 這裡使用 encodeURIComponent 然後將 %20 替換為 +，以匹配 HttpUtility.UrlEncode 的行為
 */
function urlEncodeLikeHttpUtility(str) {
    // 先使用 encodeURIComponent 編碼
    let encoded = encodeURIComponent(str);
    // 將 %20 (空格) 替換為 +，以匹配 HttpUtility.UrlEncode 的行為
    encoded = encoded.replace(/%20/g, '+');
    return encoded;
}

/**
 * 生成支付回調的簽名（CheckMacValue）
 * @param {Object} formData - 表單數據（不包含 CheckMacValue）
 * @param {string} hashKey - HashKey
 * @param {string} hashIV - HashIV
 * @returns {string} 簽名值
 */
function generatePaymentSignature(formData, hashKey, hashIV) {
    // 1. 將所有欄位（除了 CheckMacValue）按字母順序排序
    const sortedKeys = Object.keys(formData).filter(k => k !== 'CheckMacValue').sort();
    
    // 2. 構建原始字串：HashKey={key}&{排序後的欄位}&HashIV={iv}
    let rawString = `HashKey=${hashKey}&`;
    rawString += sortedKeys.map(k => `${k}=${formData[k]}`).join('&');
    rawString += `&HashIV=${hashIV}`;
    
    // console.log(`[DEBUG] 簽名生成 - 原始字串: ${rawString}`);
    
    // 3. URL 編碼並轉小寫（模擬 HttpUtility.UrlEncode 的行為）
    const encoded = urlEncodeLikeHttpUtility(rawString).toLowerCase();
    
    // console.log(`[DEBUG] 簽名生成 - URL編碼後: ${encoded}`);
    
    // 4. SHA256 雜湊並轉大寫
    const hash = crypto.sha256(encoded, 'hex');
    const signature = hash.toUpperCase();
    
    // console.log(`[DEBUG] 簽名生成 - 計算的簽名: ${signature}`);
    
    return signature;
}

/**
 * 生成支付成功的回調表單數據
 * @param {string} recordNo - 訂單號
 * @param {string} amount - 金額
 * @returns {Object} 表單數據
 */
function generatePaymentCallbackData(recordNo, amount) {
    const now = new Date();
    const merchantTradeDate = `${now.getFullYear()}/${String(now.getMonth() + 1).padStart(2, '0')}/${String(now.getDate()).padStart(2, '0')} ${String(now.getHours()).padStart(2, '0')}:${String(now.getMinutes()).padStart(2, '0')}:${String(now.getSeconds()).padStart(2, '0')}`;
    
    const formData = {
        MerchantID: PAYMENT_MERCHANT_ID,
        MerchantTradeNo: recordNo,
        MerchantTradeDate: merchantTradeDate,
        PaymentType: 'aio',
        RtnCode: '1', // 1 表示付款成功
        RtnMsg: '交易成功',
        TradeNo: `EC${Date.now()}${Math.floor(Math.random() * 10000)}`, // 綠界交易編號
        TradeAmt: amount,
        PaymentDate: merchantTradeDate,
        PaymentTypeChargeFee: '0',
        TradeDate: merchantTradeDate,
        SimulatePaid: '0', // 0 表示非模擬付款
        CustomField1: '',
        CustomField2: '',
        CustomField3: '',
        CustomField4: '',
    };
    
    // 生成簽名
    const checkMacValue = generatePaymentSignature(formData, PAYMENT_HASH_KEY, PAYMENT_HASH_IV);
    formData.CheckMacValue = checkMacValue;
    
    return formData;
}

// ===== 認證 =====
export function setup() {
    // setup 只在測試開始時執行一次，用於驗證測試環境
    console.log('✓ 混合測試（含支付回調）環境初始化完成');
    return { initialized: true };
}

// 存儲每個 VU 的 CSRF Token（使用全局變數，但每個 VU 是獨立的）
let csrfToken = null;

/**
 * 從 Set-Cookie 響應頭中提取 CSRF Token
 */
function extractCsrfTokenFromResponse(response) {
    // k6 會自動處理 cookie，但我們需要從 Set-Cookie 頭中提取 Token
    const setCookieHeaders = response.headers['Set-Cookie'];
    if (!setCookieHeaders) return null;
    
    // Set-Cookie 可能是字符串或數組
    const cookieStrings = Array.isArray(setCookieHeaders) ? setCookieHeaders : [setCookieHeaders];
    
    for (let cookieString of cookieStrings) {
        // 格式可能是: "X-CSRF-Token=value; path=/; expires=..."
        const match = cookieString.match(/X-CSRF-Token=([^;]+)/);
        if (match && match[1]) {
            return match[1];
        }
    }
    return null;
}

/**
 * 每個 VU 執行前先登入，獲取 session cookie 和 CSRF Token
 * k6 會自動處理 cookie，每個 VU 都有獨立的 cookie jar
 * 使用固定的100個測試用戶，根據 VU ID 分配
 */
function ensureAuthenticated() {
    const currentUser = getCurrentUser();
    const loginData = {
        Username: currentUser.Username,
        Password: currentUser.Password,
    };

    // console.log(`[DEBUG] VU ${__VU} 準備登入，用戶名: ${currentUser.Username}`);
    const loginResponse = http.post(`${BASE_URL}/User/UserLogin`, JSON.stringify(loginData), {
        headers: getAuthHeaders(),
    });

    // console.log(`[DEBUG] VU ${__VU} 登入響應狀態碼: ${loginResponse.status}`);
    
    if (loginResponse.status === 200) {
        try {
            const result = parseApiResponse(loginResponse);
            
            if (result.success) {
                // 從響應的 Set-Cookie 頭中提取 CSRF Token
                csrfToken = extractCsrfTokenFromResponse(loginResponse);
                
                if (csrfToken) {
                    // console.log(`[DEBUG] VU ${__VU} 登入成功，用戶: ${currentUser.Username}，已獲取 CSRF Token`);
                } else {
                    // console.log(`[WARN] VU ${__VU} 登入成功但未找到 CSRF Token`);
                    // console.log(`[DEBUG] Set-Cookie 頭: ${JSON.stringify(loginResponse.headers['Set-Cookie'])}`);
                }
                
                // k6 會自動保存 cookie，後續請求會自動帶上
                return true;
            } else {
                // console.log(`[WARN] VU ${__VU} 登入失敗: ${result.error || '未知錯誤'}`);
                // console.log(`[WARN] 請確保用戶 ${currentUser.Username} 已存在，請先執行註冊腳本`);
            }
        } catch (e) {
            // console.log(`[ERROR] VU ${__VU} 解析登入響應失敗: ${e.message}`);
        }
    } else {
        // console.log(`[WARN] VU ${__VU} 登入 HTTP 請求失敗，狀態碼: ${loginResponse.status}`);
    }
    return false;
}

/**
 * 獲取帶有 CSRF Token 的認證標頭
 */
function getAuthHeadersWithCsrf() {
    const headers = getAuthHeaders();
    if (csrfToken) {
        headers['X-CSRF-Token'] = csrfToken;
    }
    return headers;
}

// ===== 各類型場景實作 =====

// 主要讀取：商品列表 / 輕量查詢 / 首頁推薦
function executeMainRead() {
    const randomPage = Math.floor(Math.random() * 5) + 1;
    const pageSize = 20;
    const res = http.get(
        `${BASE_URL}/Product/GetProductBasicInfoList?tag=all&kind=all&query=&page=${randomPage}&pageSize=${pageSize}`,
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
        const productId = Math.floor(Math.random() * 100) + 1;
        res = http.get(
            `${BASE_URL}/Product/GetProductById?productId=${productId}`,
            {
                headers: getAuthHeaders(),
                tags: { name: 'GetProductById', traffic_type: 'secondary_read' },
            }
        );
    } else if (choice < 0.8) {
        res = http.get(
            `${BASE_URL}/Order/GetOrders?query=`,
            {
                headers: getAuthHeaders(),
                tags: { name: 'GetOrders', traffic_type: 'secondary_read' },
            }
        );
    } else {
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

    if (choice < 0.7) {
        const cartData = generateCartData();
        const res = http.post(
            `${BASE_URL}/Cart/MergeCartContent`,
            JSON.stringify(cartData),
            {
                headers: getAuthHeadersWithCsrf(),
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
        const userData = generateSignUpData();
        const res = http.post(
            `${BASE_URL}/User/UserRegister`,
            JSON.stringify(userData),
            {
                headers: getAuthHeadersWithCsrf(),
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
        // 使用當前 VU 對應的用戶登入
        const currentUser = getCurrentUser();
        const loginData = {
            Username: currentUser.Username,
            Password: currentUser.Password,
        };
        const res = http.post(
            `${BASE_URL}/User/UserLogin`,
            JSON.stringify(loginData),
            {
                headers: getAuthHeadersWithCsrf(),
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

// 重度寫入：結帳 (建立訂單) + 支付成功回調
function executeHeavyWrite() {
    // 0. 確保已登入（每個 VU 都需要自己的 session）
    const isAuthenticated = ensureAuthenticated();
    if (!isAuthenticated) {
        console.log(`[WARN] ⚠ 登入失敗，跳過 SubmitOrder`);
        return;
    }
    
    // 1. 提交訂單（需要帶上 CSRF Token）
    const orderData = generateOrderData();
    const submitOrderRes = http.post(
        `${BASE_URL}/Order/SubmitOrder`,
        JSON.stringify(orderData),
        {
            headers: getAuthHeadersWithCsrf(),
            tags: { name: 'SubmitOrder', traffic_type: 'heavy_write' },
        }
    );

    const submitOrderOk = check(submitOrderRes, {
        '結帳 / 建立訂單 HTTP 可處理': (r) => [200, 400, 401, 403, 429, 500, 503].includes(r.status),
    });

    const submitOrderRealErr = !submitOrderOk && isRealError(submitOrderRes.status);
    errorRate.add(submitOrderRealErr ? 1 : 0);
    realErrorRate.add(isRealError(submitOrderRes.status) ? 1 : 0);
    responseTimeTrend.add(submitOrderRes.timings.duration);
    heavyWriteTime.add(submitOrderRes.timings.duration);
    heavyWriteHit.add(1);

    // 2. 如果訂單提交成功，發送支付成功回調
    if (submitOrderRes.status === 200) {
        // console.log(`[DEBUG] SubmitOrder HTTP 狀態碼: ${submitOrderRes.status}`);
        // console.log(`[DEBUG] SubmitOrder 響應內容 (前500字符): ${submitOrderRes.body.substring(0, 500)}`);
        
        try {
            const submitOrderResult = parseApiResponse(submitOrderRes);
            // console.log(`[DEBUG] 解析結果 - success: ${submitOrderResult.success}, 是否有 data: ${!!submitOrderResult.data}`);
            
            if (submitOrderResult.success && submitOrderResult.data) {
                // console.log(`[DEBUG] Data 完整內容: ${JSON.stringify(submitOrderResult.data)}`);
                
                // 嘗試多種可能的字段名（C# 可能使用 camelCase 或 PascalCase）
                const recordNo = submitOrderResult.data.recordNo || 
                                submitOrderResult.data.RecordNo || 
                                submitOrderResult.data.record_no;
                const amount = submitOrderResult.data.amount || 
                              submitOrderResult.data.Amount;
                
                // console.log(`[DEBUG] 提取的訂單號: ${recordNo}`);
                // console.log(`[DEBUG] 提取的金額: ${amount}`);

                if (recordNo && amount) {
                    // console.log(`[INFO] ✓ SubmitOrder 成功，訂單號: ${recordNo}, 金額: ${amount}`);
                    // console.log(`[INFO] 準備發送支付成功回調到 ECPayReturn...`);
                    
                    // 等待一小段時間，模擬支付處理時間
                    randomSleep(0.5, 1.5);

                    // 生成支付成功回調數據
                    const callbackData = generatePaymentCallbackData(recordNo, amount);
                    // console.log(`[DEBUG] 生成的回調數據: ${JSON.stringify(callbackData)}`);

                    // 將對象轉換為 form-urlencoded 字符串
                    const formDataString = Object.keys(callbackData)
                        .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(callbackData[key])}`)
                        .join('&');
                    
                    // console.log(`[DEBUG] Form-urlencoded 字符串長度: ${formDataString.length}`);
                    // console.log(`[DEBUG] 準備發送 POST 請求到: ${BASE_URL}/Payment/ECPayReturn`);

                    // 發送支付成功回調
                    const callbackRes = http.post(
                        `${BASE_URL}/Payment/ECPayReturn`,
                        formDataString,
                        {
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded',
                            },
                            tags: { name: 'ECPayReturn', traffic_type: 'payment_callback' },
                        }
                    );

                    // console.log(`[DEBUG] ECPayReturn 響應狀態碼: ${callbackRes.status}`);
                    // console.log(`[DEBUG] ECPayReturn 響應內容 (前500字符): ${callbackRes.body.substring(0, 500)}`);

                    const callbackOk = check(callbackRes, {
                        '支付回調 HTTP 可處理': (r) => [200, 400, 500, 503].includes(r.status),
                    });

                    const callbackRealErr = !callbackOk && isRealError(callbackRes.status);
                    errorRate.add(callbackRealErr ? 1 : 0);
                    realErrorRate.add(isRealError(callbackRes.status) ? 1 : 0);
                    responseTimeTrend.add(callbackRes.timings.duration);
                    paymentCallbackTime.add(callbackRes.timings.duration);
                    paymentCallbackHit.add(1);

                    // if (callbackRes.status === 200) {
                    //     console.log(`[SUCCESS] ✓ 支付回調成功: 訂單號 ${recordNo}`);
                    // } else {
                    //     console.log(`[WARN] ⚠ 支付回調失敗: 訂單號 ${recordNo}, 狀態碼 ${callbackRes.status}`);
                    //     console.log(`[WARN] 響應內容: ${callbackRes.body.substring(0, 300)}`);
                    // }
                } else {
                    // console.log(`[ERROR] ✗ 無法提取訂單號或金額 - recordNo: ${recordNo}, amount: ${amount}`);
                    // console.log(`[ERROR] Data 結構: ${JSON.stringify(submitOrderResult.data)}`);
                }
            } else {
                // console.log(`[ERROR] ✗ SubmitOrder 響應解析失敗或數據為空`);
                // console.log(`[ERROR] 解析結果: ${JSON.stringify(submitOrderResult)}`);
            }
        } catch (e) {
            // console.log(`[ERROR] ✗ 解析訂單提交響應時發生異常: ${e.message}`);
            // console.log(`[ERROR] 堆疊追蹤: ${e.stack}`);
            // console.log(`[ERROR] 原始響應: ${submitOrderRes.body.substring(0, 500)}`);
        }
    } else {
        // console.log(`[WARN] ⚠ SubmitOrder 失敗，HTTP 狀態碼: ${submitOrderRes.status}`);
        // console.log(`[WARN] 響應內容 (前300字符): ${submitOrderRes.body.substring(0, 300)}`);
    }

    // 重度寫入之間睡久一點，貼近真實結帳行為
    randomSleep(1, 3);
}

// ===== 主流程：依比例隨機選擇一種場景執行 =====
export default function () {
    // k6 會自動處理 cookie，每個 VU 都有獨立的 cookie jar
    // 對於需要認證的操作，會在執行前先登入
    
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

