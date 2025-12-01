/**
 * k6 測試工具函數
 * 提供共用的輔助函數，用於所有測試腳本
 */

import { sleep } from 'k6';

/**
 * 獲取 API 基礎 URL
 * 優先使用環境變數，否則使用預設值
 */
export function getBaseUrl() {
    return __ENV.BASE_URL || 'http://localhost:5025';
}

/**
 * 獲取認證標頭
 * 根據實際的認證方式修改此函數
 */
export function getAuthHeaders(token = null) {
    const headers = {
        'Content-Type': 'application/json',
    };

    // 如果有提供 token，添加到標頭
    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    // TODO: 如果需要其他認證方式，請在此添加
    // 例如：Cookie 認證
    // headers['Cookie'] = 'session_id=xxx';

    return headers;
}

/**
 * 生成隨機用戶 ID（用於測試）
 */
export function getRandomUserId() {
    // 返回 1-1000 之間的隨機用戶 ID
    return Math.floor(Math.random() * 1000) + 1;
}

/**
 * 生成隨機訂單編號
 */
export function generateOrderCode() {
    const timestamp = Date.now();
    const random = Math.floor(Math.random() * 10000);
    return `ORD${timestamp}${random}`;
}

/**
 * 生成隨機商品 ID
 */
export function getRandomProductId() {
    // 返回 1-100 之間的隨機商品 ID
    return Math.floor(Math.random() * 100) + 1;
}

/**
 * 檢查響應是否成功
 */
export function checkResponseStatus(response, expectedStatus = 200) {
    if (response.status !== expectedStatus) {
        console.error(`預期狀態碼 ${expectedStatus}，實際為 ${response.status}`);
        console.error(`響應內容: ${response.body}`);
        return false;
    }
    return true;
}

/**
 * 解析 API 響應
 * 根據實際的 API 響應格式修改此函數
 * API 響應格式: { Code: 1 (成功), Message: "請求成功", Data: {...} }
 * RespCode: SUCCESS = 1, FAIL = -1, UN_AUTHORIZED = 0
 */
export function parseApiResponse(response) {
    // 檢查響應是否為空
    if (!response.body || response.body.trim() === '') {
        return { success: false, error: '響應為空', status: response.status };
    }
    
    // 檢查響應是否為 JSON 格式
    const bodyTrimmed = response.body.trim();
    if (!bodyTrimmed.startsWith('{') && !bodyTrimmed.startsWith('[')) {
        // 不是 JSON 格式，可能是 HTML 錯誤頁面或其他格式
        return { 
            success: false, 
            error: `非 JSON 響應格式 (狀態碼: ${response.status})`, 
            status: response.status,
            raw: bodyTrimmed.substring(0, 200) // 只取前 200 個字符
        };
    }
    
    try {
        const json = JSON.parse(response.body);
        
        // 根據實際 API 響應格式調整
        // API 使用 RespCode.SUCCESS = 1 表示成功
        if (json.Code === 1 || json.code === 1 || json.Code === '1' || json.code === '1') {
            return { success: true, data: json.Data || json.data, message: json.Message || json.message };
        } else {
            return { success: false, error: json.Message || json.message || '未知錯誤', data: json, status: response.status };
        }
    } catch (e) {
        // JSON 解析失敗
        return { 
            success: false, 
            error: `JSON 解析失敗: ${e.message}`, 
            status: response.status,
            raw: bodyTrimmed.substring(0, 200) // 只取前 200 個字符以避免日誌過長
        };
    }
}

/**
 * 隨機延遲（模擬真實用戶行為）
 */
export function randomSleep(minMs = 1000, maxMs = 3000) {
    const delay = Math.floor(Math.random() * (maxMs - minMs + 1)) + minMs;
    sleep(delay / 1000); // k6 的 sleep 使用秒為單位
}

/**
 * 生成測試用的用戶註冊數據
 */
export function generateTestUser() {
    const timestamp = Date.now();
    const random = Math.floor(Math.random() * 10000);
    
    return {
        email: `testuser${timestamp}${random}@example.com`,
        password: 'Test123456!',
        name: `測試用戶${random}`,
        phone: `09${Math.floor(Math.random() * 100000000)}`,
    };
}

/**
 * 生成測試用的購物車數據（符合 CartDTO 格式）
 */
export function generateCartData() {
    const itemCount = Math.floor(Math.random() * 5) + 1; // 1-5 個商品
    const items = [];
    
    for (let i = 0; i < itemCount; i++) {
        items.push({
            ProductVariantId: Math.floor(Math.random() * 100) + 1, // 1-100 的變體 ID
            Quantity: Math.floor(Math.random() * 3) + 1, // 1-3 個
        });
    }
    
    return {
        Items: items,
        IsCover: false, // 是否覆蓋現有購物車
    };
}

/**
 * 生成測試用的訂單數據（符合 SubmitOrderReq 格式）
 */
export function generateOrderData() {
    const itemCount = Math.floor(Math.random() * 3) + 1; // 1-3 個商品
    const items = [];
    
    for (let i = 0; i < itemCount; i++) {
        items.push({
            ProductId: getRandomProductId(),
            VariantId: Math.floor(Math.random() * 100) + 1, // 1-100 的變體 ID
            Quantity: Math.floor(Math.random() * 2) + 1, // 1-2 個
        });
    }
    
    return {
        Items: items,
        ShippingFee: 60, // 運費
        ShippingAddress: '台北市中正區測試路123號',
        RecieveStore: '7-11 測試門市',
        RecieveWay: 'convenience_store', // 超商取貨
        ReceiverName: `測試收件人${Math.floor(Math.random() * 1000)}`,
        ReceiverPhone: `09${Math.floor(Math.random() * 100000000).toString().padStart(8, '0')}`,
        Email: `test${Date.now()}${Math.floor(Math.random() * 1000)}@example.com`,
    };
}

/**
 * 生成測試用的註冊數據（符合 SignUpDTO 格式）
 */
export function generateSignUpData() {
    const timestamp = Date.now();
    const random = Math.floor(Math.random() * 10000);
    
    return {
        Username: `testuser${timestamp}${random}`,
        Email: `testuser${timestamp}${random}@example.com`,
        NickName: `測試用戶${random}`,
        Password: 'Test123456!',
    };
}

/**
 * 生成測試用的登入數據（符合 LoginDTO 格式）
 * 注意：這需要實際存在的用戶，用於測試時可能需要先註冊
 */
export function generateLoginData(username = null, password = null) {
    return {
        Username: username || 'testuser',
        Password: password || 'Test123456!',
    };
}

/**
 * 等待指定時間（秒）
 */
export function wait(seconds) {
    sleep(seconds);
}

/**
 * 記錄測試指標
 */
export function recordMetric(name, value, tags = {}) {
    // k6 會自動收集指標，此函數可用於自定義指標
    // 使用方式: recordMetric('custom_metric', 123, { tag: 'value' });
}

