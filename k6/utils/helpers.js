/**
 * k6 測試工具函數
 * 提供共用的輔助函數，用於所有測試腳本
 */

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
 */
export function parseApiResponse(response) {
    try {
        const json = JSON.parse(response.body);
        
        // 根據實際 API 響應格式調整
        // 假設響應格式為: { code: 200, data: {...}, message: "..." }
        if (json.code === 200 || json.code === '200') {
            return { success: true, data: json.data, message: json.message };
        } else {
            return { success: false, error: json.message || '未知錯誤', data: json };
        }
    } catch (e) {
        console.error('解析響應失敗:', e);
        return { success: false, error: '解析響應失敗', raw: response.body };
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
 * 生成測試用的購物車數據
 */
export function generateCartData() {
    const itemCount = Math.floor(Math.random() * 5) + 1; // 1-5 個商品
    const items = [];
    
    for (let i = 0; i < itemCount; i++) {
        items.push({
            productId: getRandomProductId(),
            variantId: Math.floor(Math.random() * 10) + 1,
            quantity: Math.floor(Math.random() * 3) + 1, // 1-3 個
        });
    }
    
    return {
        items: items,
        isCover: false, // 是否覆蓋現有購物車
    };
}

/**
 * 生成測試用的訂單數據
 */
export function generateOrderData() {
    return {
        shippingAddress: {
            name: '測試收件人',
            phone: '0912345678',
            address: '測試地址 123 號',
            city: '台北市',
            district: '中正區',
            zipCode: '100',
        },
        paymentMethod: 'credit_card', // 或 'ecpay', 'cash_on_delivery'
        cartItems: generateCartData().items,
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

