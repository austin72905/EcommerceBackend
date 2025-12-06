/**
 * 冒煙測試 (Smoke Test)
 * 
 * 目的: 快速驗證系統基本功能是否正常
 * 負載: 1 個虛擬用戶，持續 1 分鐘
 * 
 * 執行方式:
 *   k6 run k6/scripts/smoke-test.js
 * 
 * 或指定環境變數:
 *   BASE_URL=http://localhost:5025 k6 run k6/scripts/smoke-test.js
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { getBaseUrl, getAuthHeaders, parseApiResponse } from '../utils/helpers.js';

// 測試配置
export const options = {
    stages: [
        { duration: '30s', target: 1 }, // 30 秒內增加到 1 個用戶
        { duration: '30s', target: 1 }, // 保持 1 個用戶 30 秒
    ],
    thresholds: {
        http_req_duration: ['p(95)<2000'], // 95% 的請求應在 2 秒內完成
        http_req_failed: ['rate<0.01'],    // 錯誤率應小於 1%
    },
};

const BASE_URL = getBaseUrl();

export default function () {
    // 測試 1: 健康檢查或基本端點
    console.log(`測試 API: ${BASE_URL}`);
    
    // 測試 2: 獲取商品列表（不需要認證，使用分頁查詢）
    const productResponse = http.get(`${BASE_URL}/Product/GetProductList?tag=test&kind=all&query=&page=1&pageSize=20`, {
        headers: getAuthHeaders(),
    });
    
    check(productResponse, {
        '商品列表請求成功': (r) => r.status === 200,
        '響應時間 < 2s': (r) => r.timings.duration < 2000,
    });
    
    const productResult = parseApiResponse(productResponse);
    if (productResult.success) {
        console.log('✓ 商品列表 API 正常');
    } else {
        console.error('✗ 商品列表 API 失敗:', productResult.error);
    }
    
    sleep(1);
    
    // 測試 3: 獲取商品基本信息（不需要認證，使用分頁查詢）
    const productBasicResponse = http.get(`${BASE_URL}/Product/GetProductBasicInfoList?tag=test&kind=all&query=&page=1&pageSize=20`, {
        headers: getAuthHeaders(),
    });
    
    check(productBasicResponse, {
        '商品基本信息請求成功': (r) => r.status === 200,
    });
    
    sleep(1);
    
    // 測試 4: 測試需要認證的端點（如果沒有 token，這會失敗，但可以驗證端點存在）
    // 注意: 實際測試時需要有效的認證 token
    const orderResponse = http.get(`${BASE_URL}/Order/GetOrders?query=`, {
        headers: getAuthHeaders(),
    });
    
    // 這個端點需要認證，所以可能返回 401，這是正常的
    check(orderResponse, {
        '訂單列表端點可訪問': (r) => r.status === 200 || r.status === 401,
    });
    
    sleep(1);
}

export function handleSummary(data) {
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}

function textSummary(data, options) {
    // 簡單的文本摘要
    return `
    ====================
    冒煙測試結果摘要
    ====================
    總請求數: ${data.metrics.http_reqs.values.count}
    平均響應時間: ${data.metrics.http_req_duration.values.avg.toFixed(2)}ms
    錯誤率: ${(data.metrics.http_req_failed.values.rate * 100).toFixed(2)}%
    ====================
    `;
}

