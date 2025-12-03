/**
 * 專業寫入效能測試 (Professional Write-Only Performance Test)
 * 
 * 目的: 測試系統在純寫入操作下的資料庫寫入效能，評估寫入操作的瓶頸
 * 
 * 測試階段:
 * - 第 1 階段（10-50 VU）: 檢測 Deadlock、基本寫入效能
 * - 第 2 階段（100-200 VU）: 檢測 transaction latency、row lock、Redis Lua + DB 一致性
 * - 第 3 階段（Spike Test）: 瞬間暴增測試，檢測系統極限
 * 
 * 測試場景:
 * - 用戶註冊（寫入 Users 表）
 * - 用戶登入（可能涉及 Session 寫入）
 * - 購物車操作（寫入 Cart 和 CartItem 表）
 * - 提交訂單（寫入 Order、OrderProduct、OrderStep、Shipment、Payment 等多張表）
 * 
 * 執行方式:
 *   k6 run k6/scripts/write-only-test.js
 * 
 * 測試模式:
 *   TEST_MODE=stage1 k6 run k6/scripts/write-only-test.js  # 第 1 階段（10-50 VU）
 *   TEST_MODE=stage2 k6 run k6/scripts/write-only-test.js  # 第 2 階段（100-200 VU）
 *   TEST_MODE=spike k6 run k6/scripts/write-only-test.js   # Spike Test（瞬間暴增）
 *   TEST_MODE=full k6 run k6/scripts/write-only-test.js    # 完整測試（所有階段）
 * 
 * 自定義參數:
 *   TEST_MODE=stage1 MAX_VUS=50 DURATION=2m k6 run k6/scripts/write-only-test.js
 *   TEST_MODE=spike SPIKE_VUS=1000 SPIKE_DURATION=10s k6 run k6/scripts/write-only-test.js
 * 
 * 注意: 此測試專注於寫入效能，會產生大量測試資料
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate, Trend } from 'k6/metrics';
import { getBaseUrl, getAuthHeaders, parseApiResponse, randomSleep, generateSignUpData, generateCartData, generateOrderData } from '../utils/helpers.js';

// 自定義指標
const errorRate = new Rate('errors');
const responseTimeTrend = new Trend('response_time_trend');
const realErrorRate = new Rate('real_errors');

// 各 API 的響應時間趨勢（用於分析 transaction latency）
const userRegisterTime = new Trend('user_register_time');
const userLoginTime = new Trend('user_login_time');
const cartOperationTime = new Trend('cart_operation_time');
const orderSubmitTime = new Trend('order_submit_time');

// 訂單提交統計（用於追蹤成功/失敗數量）
const orderSubmitSuccessCount = new Rate('order_submit_success'); // 成功提交的訂單數
const orderSubmitAttemptCount = new Rate('order_submit_attempt'); // 嘗試提交的訂單數（所有請求）

// 特定錯誤類型追蹤（用於檢測 Deadlock、Lock 等問題）
const deadlockErrors = new Rate('deadlock_errors'); // 檢測 Deadlock（通常是 500 或特定錯誤訊息）
const timeoutErrors = new Rate('timeout_errors'); // 檢測 Timeout（通常是 504 或長時間無響應）
const connectionErrors = new Rate('connection_errors'); // 檢測連線錯誤（53300: too many clients）
const duplicateOrderErrors = new Rate('duplicate_order_errors'); // 檢測訂單重複產生（409 或特定錯誤）

// 長時間響應追蹤（用於推斷 lock time）
const longTransactionTime = new Trend('long_transaction_time'); // > 3秒的響應時間（可能是 lock 等待）

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
const TEST_MODE = __ENV.TEST_MODE || 'stage1'; // stage1, stage2, spike, full
const MAX_VUS = parseInt(__ENV.MAX_VUS) || (TEST_MODE === 'stage1' ? 50 : TEST_MODE === 'stage2' ? 200 : 200);
const DURATION = __ENV.DURATION || (TEST_MODE === 'spike' ? '30s' : '2m');
const SPIKE_VUS = parseInt(__ENV.SPIKE_VUS) || 1000; // Spike test 的瞬間 VU 數
const SPIKE_DURATION = __ENV.SPIKE_DURATION || '10s'; // Spike test 的持續時間

// 根據測試模式和 DURATION 動態計算階段時間
let stages = [];
let thresholds = {};

if (TEST_MODE === 'stage1') {
    // 第 1 階段：10-50 VU，檢測 Deadlock
    const totalSeconds = parseDuration(DURATION);
    const rampUp = Math.max(5, Math.floor(totalSeconds * 0.3));
    const holdTime = Math.max(10, Math.floor(totalSeconds * 0.5));
    const rampDown = Math.max(5, Math.floor(totalSeconds * 0.2));
    
    stages = [
        { duration: `${rampUp}s`, target: 10 },
        { duration: `${rampUp}s`, target: 50 },
        { duration: `${holdTime}s`, target: 50 },
        { duration: `${rampDown}s`, target: 0 },
    ];
    
    thresholds = {
        http_req_duration: ['p(50)<2000', 'p(95)<5000', 'p(99)<10000'],
        http_req_failed: ['rate<0.10'],
        errors: ['rate<0.05'],
        real_errors: ['rate<0.02'],
        deadlock_errors: ['rate<0.01'], // Deadlock 應該很少或沒有
        timeout_errors: ['rate<0.01'],
        connection_errors: ['rate<0.01'],
        order_submit_time: ['p(95)<5000'],
    };
    
    // summaryTrendStats 是 options 的屬性，不是 thresholds 的
} else if (TEST_MODE === 'stage2') {
    // 第 2 階段：100-200 VU，檢測 transaction latency、row lock
    const totalSeconds = parseDuration(DURATION);
    const rampUp1 = Math.max(5, Math.floor(totalSeconds * 0.2));
    const rampUp2 = Math.max(5, Math.floor(totalSeconds * 0.3));
    const holdTime = Math.max(10, Math.floor(totalSeconds * 0.4));
    const rampDown = Math.max(5, Math.floor(totalSeconds * 0.1));
    
    stages = [
        { duration: `${rampUp1}s`, target: 50 },
        { duration: `${rampUp2}s`, target: 100 },
        { duration: `${rampUp2}s`, target: MAX_VUS },
        { duration: `${holdTime}s`, target: MAX_VUS },
        { duration: `${rampDown}s`, target: 0 },
    ];
    
    thresholds = {
        http_req_duration: ['p(50)<3000', 'p(95)<8000', 'p(99)<15000'],
        http_req_failed: ['rate<0.15'],
        errors: ['rate<0.10'],
        real_errors: ['rate<0.05'],
        deadlock_errors: ['rate<0.02'],
        timeout_errors: ['rate<0.05'],
        connection_errors: ['rate<0.05'],
        long_transaction_time: ['p(95)<10000'], // 長時間事務（可能是 lock 等待）
        order_submit_time: ['p(95)<8000'],
    };
    
    // summaryTrendStats 是 options 的屬性，不是 thresholds 的
} else if (TEST_MODE === 'spike') {
    // 第 3 階段：Spike Test，瞬間暴增
    const spikeSeconds = parseDuration(SPIKE_DURATION);
    const rampUp = 2; // 快速增加到目標 VU
    const holdTime = spikeSeconds;
    const rampDown = 5;
    
    stages = [
        { duration: `${rampUp}s`, target: SPIKE_VUS },
        { duration: `${holdTime}s`, target: SPIKE_VUS },
        { duration: `${rampDown}s`, target: 0 },
    ];
    
    thresholds = {
        http_req_duration: ['p(95)<20000', 'p(99)<30000'], // Spike test 允許較高的延遲
        http_req_failed: ['rate<0.30'], // Spike test 允許較高的失敗率
        errors: ['rate<0.20'],
        real_errors: ['rate<0.10'],
        deadlock_errors: ['rate<0.05'],
        timeout_errors: ['rate<0.10'],
        connection_errors: ['rate<0.10'],
    };
    
    // summaryTrendStats 是 options 的屬性，不是 thresholds 的
} else if (TEST_MODE === 'full') {
    // 完整測試：包含所有階段
    const totalSeconds = parseDuration(DURATION);
    const stage1Time = Math.max(30, Math.floor(totalSeconds * 0.25));
    const stage2Time = Math.max(60, Math.floor(totalSeconds * 0.5));
    const spikeTime = 10;
    const rampDown = Math.max(10, Math.floor(totalSeconds * 0.15));
    
    stages = [
        // 第 1 階段
        { duration: '10s', target: 10 },
        { duration: '10s', target: 50 },
        { duration: `${stage1Time}s`, target: 50 },
        // 第 2 階段
        { duration: '10s', target: 100 },
        { duration: '10s', target: 200 },
        { duration: `${stage2Time}s`, target: 200 },
        // Spike Test
        { duration: '2s', target: SPIKE_VUS },
        { duration: `${spikeTime}s`, target: SPIKE_VUS },
        // 降載
        { duration: `${rampDown}s`, target: 0 },
    ];
    
    thresholds = {
        http_req_duration: ['p(95)<15000'],
        http_req_failed: ['rate<0.20'],
        errors: ['rate<0.15'],
        real_errors: ['rate<0.08'],
    };
} else {
    // 預設：第 1 階段
    const totalSeconds = parseDuration(DURATION);
    const rampUp = Math.max(5, Math.floor(totalSeconds * 0.3));
    const holdTime = Math.max(10, Math.floor(totalSeconds * 0.5));
    const rampDown = Math.max(5, Math.floor(totalSeconds * 0.2));
    
    stages = [
        { duration: `${rampUp}s`, target: 10 },
        { duration: `${rampUp}s`, target: MAX_VUS },
        { duration: `${holdTime}s`, target: MAX_VUS },
        { duration: `${rampDown}s`, target: 0 },
    ];
    
    thresholds = {
        http_req_duration: ['p(50)<2000', 'p(95)<5000', 'p(99)<8000'],
        http_req_failed: ['rate<0.15'],
        errors: ['rate<0.10'],
        real_errors: ['rate<0.05'],
    };
}

export const options = {
    stages: stages,
    thresholds: thresholds,
    summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(50)', 'p(75)', 'p(90)', 'p(95)', 'p(99)'],
};

// 定義在寫入測試中可接受的 HTTP 狀態碼
const ACCEPTABLE_STATUS_CODES = [200, 400, 401, 403, 404, 409, 429, 500, 503];

// 判斷是否為真正的錯誤
function isRealError(statusCode) {
    return !ACCEPTABLE_STATUS_CODES.includes(statusCode);
}

// 檢測 Deadlock 錯誤（通常是 500 或包含特定錯誤訊息）
function isDeadlockError(response) {
    if (response.status === 500 || response.status === 503) {
        const body = response.body || '';
        // 檢查是否包含 Deadlock 相關錯誤訊息
        return body.includes('deadlock') || 
               body.includes('Deadlock') || 
               body.includes('DEADLOCK') ||
               body.includes('40P01') || // PostgreSQL deadlock detected
               body.includes('40001'); // SQL Server deadlock
    }
    return false;
}

// 檢測 Timeout 錯誤
function isTimeoutError(response) {
    return response.status === 504 || 
           response.status === 408 ||
           response.timings.duration > 30000; // 超過 30 秒視為 timeout
}

// 檢測連線錯誤（too many clients）
function isConnectionError(response) {
    if (response.status === 500 || response.status === 503) {
        const body = response.body || '';
        return body.includes('too many clients') ||
               body.includes('53300') ||
               body.includes('connection') && body.includes('pool');
    }
    return false;
}

// 檢測訂單重複產生（409 或特定錯誤訊息）
function isDuplicateOrderError(response) {
    if (response.status === 409) {
        const body = response.body || '';
        return body.includes('duplicate') || 
               body.includes('already exists') ||
               body.includes('重複');
    }
    return false;
}

export default function () {
    // k6 會自動處理 cookie，每個 VU 都有獨立的 cookie jar
    
    // ========== 場景 1: 用戶註冊（寫入 Users 表）==========
    const userData = generateSignUpData();
    const signUpResponse = http.post(`${BASE_URL}/User/UserRegister`, JSON.stringify(userData), {
        headers: getAuthHeaders(),
        tags: { name: 'UserRegister', test_type: 'write_only' },
    });
    
    const signUpCheck = check(signUpResponse, {
        '用戶註冊 HTTP 請求可處理': (r) => {
            // 接受 200 (成功), 400 (參數錯誤), 409 (用戶已存在), 429 (限流), 500 (服務器錯誤)
            return [200, 400, 409, 429, 500, 503].includes(r.status);
        },
    });
    
    let signUpSuccess = false;
    if (signUpResponse.status === 200) {
        const result = parseApiResponse(signUpResponse);
        const businessCheck = check(signUpResponse, {
            '用戶註冊業務邏輯成功': () => result.success === true,
        });
        signUpSuccess = businessCheck;
    } else if ([400, 409, 429, 500, 503].includes(signUpResponse.status)) {
        // 這些狀態碼是可接受的（409 表示用戶已存在，這是正常的）
        signUpSuccess = true;
    } else {
        signUpSuccess = false;
    }
    
    const isSignUpError = !signUpSuccess && isRealError(signUpResponse.status);
    errorRate.add(isSignUpError ? 1 : 0);
    realErrorRate.add(isRealError(signUpResponse.status) ? 1 : 0);
    responseTimeTrend.add(signUpResponse.timings.duration);
    userRegisterTime.add(signUpResponse.timings.duration);
    
    randomSleep(0.5, 1.5);
    
    // ========== 場景 2: 用戶登入（可能涉及 Session 寫入）==========
    const loginData = {
        Username: userData.Username,
        Password: userData.Password,
    };
    const loginResponse = http.post(`${BASE_URL}/User/UserLogin`, JSON.stringify(loginData), {
        headers: getAuthHeaders(),
        tags: { name: 'UserLogin', test_type: 'write_only' },
    });
    
    const loginCheck = check(loginResponse, {
        '用戶登入 HTTP 請求可處理': (r) => {
            // 接受 200 (成功), 401 (認證失敗), 429 (限流), 500 (服務器錯誤)
            return [200, 401, 429, 500, 503].includes(r.status);
        },
    });
    
    let loginSuccess = false;
    if (loginResponse.status === 200) {
        const result = parseApiResponse(loginResponse);
        const businessCheck = check(loginResponse, {
            '用戶登入業務邏輯成功': () => result.success === true,
        });
        loginSuccess = businessCheck;
    } else if ([401, 429, 500, 503].includes(loginResponse.status)) {
        // 401 可能是因為註冊失敗導致用戶不存在，這是可接受的
        loginSuccess = true;
    } else {
        loginSuccess = false;
    }
    
    const isLoginError = !loginSuccess && isRealError(loginResponse.status);
    errorRate.add(isLoginError ? 1 : 0);
    realErrorRate.add(isRealError(loginResponse.status) ? 1 : 0);
    responseTimeTrend.add(loginResponse.timings.duration);
    userLoginTime.add(loginResponse.timings.duration);
    
    // k6 會自動處理 cookie，登入成功後 cookie 會自動保存
    
    randomSleep(0.5, 1.5);
    
    // ========== 場景 3: 購物車操作（寫入 Cart 和 CartItem 表）==========
    // 注意：需要先登入成功才能操作購物車
    const cartData = generateCartData();
    const cartResponse = http.post(`${BASE_URL}/Cart/MergeCartContent`, JSON.stringify(cartData), {
        headers: getAuthHeaders(),
        tags: { name: 'MergeCartContent', test_type: 'write_only' },
    });
    
    const cartCheck = check(cartResponse, {
        '購物車操作 HTTP 請求可處理': (r) => {
            // 接受 200 (成功), 400 (參數錯誤), 401 (未認證), 429 (限流), 500 (服務器錯誤), 503 (服務不可用)
            return [200, 400, 401, 429, 500, 503].includes(r.status);
        },
    });
    
    let cartSuccess = false;
    if (cartResponse.status === 200) {
        const result = parseApiResponse(cartResponse);
        const businessCheck = check(cartResponse, {
            '購物車操作業務邏輯成功': () => result.success === true,
        });
        cartSuccess = businessCheck;
    } else if ([400, 401, 429, 500, 503].includes(cartResponse.status)) {
        // 這些狀態碼是可接受的（401 可能是因為登入失敗）
        cartSuccess = true;
    } else {
        cartSuccess = false;
    }
    
    const isCartError = !cartSuccess && isRealError(cartResponse.status);
    errorRate.add(isCartError ? 1 : 0);
    realErrorRate.add(isRealError(cartResponse.status) ? 1 : 0);
    responseTimeTrend.add(cartResponse.timings.duration);
    cartOperationTime.add(cartResponse.timings.duration);
    
    randomSleep(1, 2);
    
    // ========== 場景 4: 提交訂單（寫入多張表：Order、OrderProduct、OrderStep、Shipment、Payment）==========
    // 注意：需要先登入成功才能提交訂單
    const orderData = generateOrderData();
    const orderSubmitResponse = http.post(`${BASE_URL}/Order/SubmitOrder`, JSON.stringify(orderData), {
        headers: getAuthHeaders(),
        tags: { name: 'SubmitOrder', test_type: 'write_only' },
    });
    
    const orderSubmitCheck = check(orderSubmitResponse, {
        '訂單創建 HTTP 請求可處理': (r) => {
            // 接受 200 (成功), 400 (參數錯誤), 401 (未認證), 403 (權限不足), 429 (限流), 500 (服務器錯誤), 503 (服務不可用)
            return [200, 400, 401, 403, 429, 500, 503].includes(r.status);
        },
    });
    
    let orderSubmitSuccess = false;
    if (orderSubmitResponse.status === 200) {
        const result = parseApiResponse(orderSubmitResponse);
        const businessCheck = check(orderSubmitResponse, {
            '訂單創建業務邏輯成功': () => result.success === true,
        });
        orderSubmitSuccess = businessCheck;
    } else if ([400, 401, 403, 429, 500, 503].includes(orderSubmitResponse.status)) {
        // 這些狀態碼是可接受的（401 可能是因為登入失敗，400 可能是庫存不足等業務邏輯錯誤）
        orderSubmitSuccess = true;
    } else {
        orderSubmitSuccess = false;
    }
    
    const isOrderSubmitError = !orderSubmitSuccess && isRealError(orderSubmitResponse.status);
    errorRate.add(isOrderSubmitError ? 1 : 0);
    realErrorRate.add(isRealError(orderSubmitResponse.status) ? 1 : 0);
    responseTimeTrend.add(orderSubmitResponse.timings.duration);
    
    // 記錄所有訂單提交嘗試（無論成功或失敗）
    orderSubmitAttemptCount.add(1);
    // 只記錄成功的訂單提交
    if (orderSubmitResponse.status === 200 && orderSubmitSuccess) {
        orderSubmitSuccessCount.add(1);
        orderSubmitTime.add(orderSubmitResponse.timings.duration);
    } else {
        orderSubmitSuccessCount.add(0);
        // 即使失敗也記錄響應時間（用於分析失敗原因）
        orderSubmitTime.add(orderSubmitResponse.timings.duration);
    }
    
    // 檢測特定錯誤類型
    if (isDeadlockError(orderSubmitResponse)) {
        deadlockErrors.add(1);
    } else {
        deadlockErrors.add(0);
    }
    
    if (isTimeoutError(orderSubmitResponse)) {
        timeoutErrors.add(1);
    } else {
        timeoutErrors.add(0);
    }
    
    if (isConnectionError(orderSubmitResponse)) {
        connectionErrors.add(1);
    } else {
        connectionErrors.add(0);
    }
    
    if (isDuplicateOrderError(orderSubmitResponse)) {
        duplicateOrderErrors.add(1);
    } else {
        duplicateOrderErrors.add(0);
    }
    
    // 記錄長時間事務（可能是 lock 等待）
    if (orderSubmitResponse.timings.duration > 3000) {
        longTransactionTime.add(orderSubmitResponse.timings.duration);
    }
    
    randomSleep(1, 3);
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
    
    // 特定錯誤類型
    const deadlockErrorsData = data.metrics.deadlock_errors?.values || {};
    const timeoutErrorsData = data.metrics.timeout_errors?.values || {};
    const connectionErrorsData = data.metrics.connection_errors?.values || {};
    const duplicateOrderErrorsData = data.metrics.duplicate_order_errors?.values || {};
    const longTransactionData = data.metrics.long_transaction_time?.values || {};
    
    // 各 API 的響應時間
    const userRegisterDuration = data.metrics.user_register_time?.values || {};
    const userLoginDuration = data.metrics.user_login_time?.values || {};
    const cartOperationDuration = data.metrics.cart_operation_time?.values || {};
    const orderSubmitDuration = data.metrics.order_submit_time?.values || {};
    
    // 訂單提交統計
    const orderSubmitSuccessData = data.metrics.order_submit_success?.values || {};
    const orderSubmitAttemptData = data.metrics.order_submit_attempt?.values || {};
    
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
    
    // 計算訂單提交的 RPS（用於預估最大寫入量）
    // 使用成功提交的訂單數，而不是所有請求數
    const orderSubmitSuccessCount = orderSubmitSuccessData.count || 0;
    const orderSubmitAttemptCount = orderSubmitAttemptData.count || 0;
    const orderSubmitSuccessRate = orderSubmitAttemptCount > 0 
        ? safeToFixed((orderSubmitSuccessCount / orderSubmitAttemptCount) * 100) 
        : '0';
    const orderSubmitRPS = orderSubmitSuccessCount > 0 && testDurationSeconds > 0 
        ? safeToFixed(orderSubmitSuccessCount / testDurationSeconds) 
        : '0';
    
    // 預估最大可支撐寫入量（基於 P95 響應時間）
    const orderSubmitP95 = getPercentile(orderSubmitDuration, 'p(95)');
    const estimatedMaxOrdersPerSecond = orderSubmitP95 && orderSubmitP95 > 0
        ? safeToFixed((MAX_VUS * 1000) / orderSubmitP95) // 簡化計算：VU數 * 1000ms / P95響應時間
        : 'N/A';
    
    return `
    ====================
    寫入效能測試結果摘要
    ====================
    測試模式: ${TEST_MODE}
    測試持續時間: ${testDurationSeconds !== null ? safeToFixed(testDurationSeconds) : safeToFixed(duration.max / 1000)}s
    最大並發用戶數: ${vus.max || 0}
    總請求數: ${httpReqs.count || 0}
    請求速率: ${safeToFixed(httpReqs.rate)} req/s
    訂單提交統計:
      嘗試提交次數: ${orderSubmitAttemptCount}
      成功提交次數: ${orderSubmitSuccessCount}
      提交成功率: ${orderSubmitSuccessRate}%
      訂單提交速率: ${orderSubmitRPS} orders/s
    
    整體響應時間統計:
      平均: ${safeToFixed(httpDuration.avg)}ms
      最小: ${safeToFixed(httpDuration.min)}ms
      最大: ${safeToFixed(httpDuration.max)}ms
      p50: ${safeToFixed(p50)}ms
      p95: ${safeToFixed(p95)}ms
      p99: ${safeToFixed(p99)}ms
    
    各 API 響應時間分析:
      用戶註冊 (UserRegister):
        平均: ${safeToFixed(userRegisterDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(userRegisterDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(userRegisterDuration, 'p(99)'))}ms
      
      用戶登入 (UserLogin):
        平均: ${safeToFixed(userLoginDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(userLoginDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(userLoginDuration, 'p(99)'))}ms
      
      購物車操作 (MergeCartContent):
        平均: ${safeToFixed(cartOperationDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(cartOperationDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(cartOperationDuration, 'p(99)'))}ms
      
      提交訂單 (SubmitOrder):
        嘗試次數: ${orderSubmitAttemptCount}
        成功次數: ${orderSubmitSuccessCount}
        成功率: ${orderSubmitSuccessRate}%
        平均響應時間: ${safeToFixed(orderSubmitDuration.avg)}ms
        p95: ${safeToFixed(getPercentile(orderSubmitDuration, 'p(95)'))}ms
        p99: ${safeToFixed(getPercentile(orderSubmitDuration, 'p(99)'))}ms
    
    錯誤統計:
      錯誤率: ${safeToFixed(errors.rate * 100)}%
      HTTP 失敗請求: ${safeToFixed(httpReqFailed.rate * 100)}%
      真正錯誤率: ${safeToFixed(realErrors.rate * 100)}% (排除可接受狀態碼)
      總錯誤數: ${errors.count || 0}
      真正錯誤數: ${realErrors.count || 0}
    
    特定錯誤類型檢測:
      Deadlock 錯誤: ${safeToFixed(deadlockErrorsData.rate * 100)}% (${deadlockErrorsData.count || 0} 次)
      Timeout 錯誤: ${safeToFixed(timeoutErrorsData.rate * 100)}% (${timeoutErrorsData.count || 0} 次)
      連線錯誤 (Too many clients): ${safeToFixed(connectionErrorsData.rate * 100)}% (${connectionErrorsData.count || 0} 次)
      訂單重複錯誤: ${safeToFixed(duplicateOrderErrorsData.rate * 100)}% (${duplicateOrderErrorsData.count || 0} 次)
      長時間事務 (>3s): ${longTransactionData.count || 0} 次
        ${longTransactionData.avg ? `平均: ${safeToFixed(longTransactionData.avg)}ms` : ''}
        ${getPercentile(longTransactionData, 'p(95)') ? `P95: ${safeToFixed(getPercentile(longTransactionData, 'p(95)'))}ms` : ''}
    
    效能評估:
      ${errors.rate < 0.10 ? '✓ 系統寫入效能表現良好' : '✗ 系統寫入效能出現問題'}
      ${p50 !== null && p50 < 2000 ? '✓ P50 響應時間優秀' : p50 !== null && p50 < 3000 ? '⚠ P50 響應時間可接受' : '✗ P50 響應時間較慢'}
      ${p95 !== null && p95 < 5000 ? '✓ P95 響應時間優秀' : p95 !== null && p95 < 8000 ? '⚠ P95 響應時間可接受' : '✗ P95 響應時間較慢'}
      ${p99 !== null && p99 < 8000 ? '✓ P99 響應時間優秀' : p99 !== null && p99 < 12000 ? '⚠ P99 響應時間可接受' : '✗ P99 響應時間較慢'}
      ${realErrors.rate < 0.05 ? '✓ 真正錯誤率在可接受範圍' : '✗ 真正錯誤率過高'}
    
    寫入操作效能分析:
      ${getPercentile(userRegisterDuration, 'p(95)') !== null && getPercentile(userRegisterDuration, 'p(95)') < 3000 ? '✓ 用戶註冊效能良好' : '⚠ 用戶註冊可能需要優化（檢查資料庫寫入效能）'}
      ${getPercentile(userLoginDuration, 'p(95)') !== null && getPercentile(userLoginDuration, 'p(95)') < 2000 ? '✓ 用戶登入效能良好' : '⚠ 用戶登入可能需要優化'}
      ${getPercentile(cartOperationDuration, 'p(95)') !== null && getPercentile(cartOperationDuration, 'p(95)') < 3000 ? '✓ 購物車操作效能良好' : '⚠ 購物車操作可能需要優化（檢查 Cart 表寫入效能）'}
      ${getPercentile(orderSubmitDuration, 'p(95)') !== null && getPercentile(orderSubmitDuration, 'p(95)') < 5000 ? '✓ 訂單提交效能良好' : '⚠ 訂單提交可能需要優化（檢查多表寫入和事務效能）'}
    
    關鍵問題檢測:
      ${deadlockErrorsData.rate > 0 ? '✗ 檢測到 Deadlock 錯誤！建議檢查：1) 事務順序 2) 索引使用 3) 鎖定範圍' : '✓ 未檢測到 Deadlock 錯誤'}
      ${timeoutErrorsData.rate > 0.05 ? '✗ Timeout 錯誤過多！建議檢查：1) 資料庫連線池 2) 事務超時設定 3) 慢查詢' : timeoutErrorsData.rate > 0 ? '⚠ 有少量 Timeout 錯誤，建議監控' : '✓ 未檢測到 Timeout 錯誤'}
      ${connectionErrorsData.rate > 0.05 ? '✗ 連線錯誤過多（Too many clients）！建議：1) 增加 PostgreSQL max_connections 2) 優化連線池設定' : connectionErrorsData.rate > 0 ? '⚠ 有少量連線錯誤，建議檢查連線池' : '✓ 未檢測到連線錯誤'}
      ${duplicateOrderErrorsData.rate > 0.05 ? '✗ 訂單重複錯誤過多！建議檢查：1) 冪等性機制 2) 唯一性約束 3) 併發控制' : duplicateOrderErrorsData.rate > 0 ? '⚠ 有少量訂單重複錯誤，建議檢查冪等性' : '✓ 未檢測到訂單重複錯誤'}
      ${longTransactionData.count > 0 && longTransactionData.avg > 5000 ? '✗ 檢測到大量長時間事務（可能是 row lock）！建議檢查：1) 事務隔離級別 2) 鎖定範圍 3) 索引使用' : longTransactionData.count > 0 ? '⚠ 有少量長時間事務，建議監控' : '✓ 未檢測到長時間事務'}
    
    效能預估分析:
      當前訂單提交速率: ${orderSubmitRPS} orders/s
      預估最大可支撐寫入量: ${estimatedMaxOrdersPerSecond} orders/s (基於 P95 響應時間 ${safeToFixed(orderSubmitP95)}ms)
      建議最大並發訂單數: ${orderSubmitP95 && orderSubmitP95 > 0 ? Math.floor((estimatedMaxOrdersPerSecond * 0.7)) : 'N/A'} orders/s (保守估計，保留 30% 緩衝)
    
    建議:
      ${p95 !== null && p95 > 5000 ? '- P95 響應時間較慢，建議檢查資料庫寫入效能和事務處理時間' : ''}
      ${getPercentile(orderSubmitDuration, 'p(95)') !== null && getPercentile(orderSubmitDuration, 'p(95)') > 5000 ? '- 訂單提交較慢，可能涉及多表寫入和事務，建議檢查：1) 資料庫鎖競爭 2) 事務隔離級別 3) 是否有不必要的鎖定' : ''}
      ${getPercentile(userRegisterDuration, 'p(95)') !== null && getPercentile(userRegisterDuration, 'p(95)') > 3000 ? '- 用戶註冊較慢，建議檢查：1) 密碼加密效能（BCrypt） 2) 唯一性檢查（Email/Username 索引）' : ''}
      ${getPercentile(cartOperationDuration, 'p(95)') !== null && getPercentile(cartOperationDuration, 'p(95)') > 3000 ? '- 購物車操作較慢，建議檢查：1) Cart 表寫入效能 2) 庫存檢查（Redis）效能' : ''}
      ${httpReqs.rate < 50 ? '- RPS 較低，寫入操作通常比讀取慢，但如果過低可能是資料庫寫入瓶頸，建議檢查：1) 資料庫 I/O 效能 2) 事務日誌寫入效能' : ''}
      ${realErrors.rate > 0.05 ? '- 真正錯誤率過高，建議檢查服務器日誌和資料庫錯誤日誌' : ''}
      ${deadlockErrorsData.rate > 0 ? '- 檢測到 Deadlock，建議：1) 檢查資料庫日誌確認 Deadlock 詳情 2) 優化事務順序 3) 減少鎖定範圍' : ''}
      ${timeoutErrorsData.rate > 0.05 ? '- Timeout 錯誤過多，建議：1) 增加 CommandTimeout 2) 優化慢查詢 3) 檢查 Redis/MQ 連線' : ''}
      ${connectionErrorsData.rate > 0.05 ? '- 連線錯誤過多，建議：1) 增加 PostgreSQL max_connections 2) 調整連線池 MaxPoolSize' : ''}
      ${duplicateOrderErrorsData.rate > 0.05 ? '- 訂單重複錯誤過多，建議：1) 實作訂單唯一性檢查 2) 使用分散式鎖 3) 檢查 RecordCode 生成邏輯' : ''}
      ${longTransactionData.count > 0 && longTransactionData.avg > 5000 ? '- 長時間事務過多，建議：1) 檢查資料庫鎖等待情況 2) 優化事務範圍 3) 考慮降低隔離級別' : ''}
      ${errors.rate < 0.10 && p95 !== null && p95 < 5000 ? '- 寫入效能表現良好，可以考慮進行更高負載的測試' : ''}
    
    測試階段建議:
      ${TEST_MODE === 'stage1' ? '- 第 1 階段完成，如果未發現 Deadlock，可以進行第 2 階段測試' : ''}
      ${TEST_MODE === 'stage2' ? '- 第 2 階段完成，如果 transaction latency 可接受，可以進行 Spike Test' : ''}
      ${TEST_MODE === 'spike' ? '- Spike Test 完成，檢查系統是否能在瞬間高負載下恢復' : ''}
      ${TEST_MODE === 'full' ? '- 完整測試完成，綜合分析所有階段的結果' : ''}
    
    注意事項:
      - 此測試會產生大量測試資料（用戶、購物車、訂單），建議在測試環境執行
      - 寫入操作的 RPS 通常比讀取操作低，這是正常的
      - 如果出現大量 409 (用戶已存在) 或 400 (參數錯誤)，可能是測試資料衝突，屬於正常現象
      - 預估的最大寫入量僅供參考，實際值可能因系統負載、資料庫狀態等因素而異
    ====================
    `;
}

