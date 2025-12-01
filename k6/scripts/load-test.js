/**
 * 負載測試 (Load Test)
 * 
 * 目的: 測試系統在正常預期負載下的表現
 * 負載: 10 個虛擬用戶，持續 5 分鐘
 * 
 * 執行方式:
 *   k6 run k6/scripts/load-test.js
 * 
 * 自定義參數:
 *   BASE_URL=http://localhost:5025 VUS=20 DURATION=10m k6 run k6/scripts/load-test.js
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate } from 'k6/metrics';
import { getBaseUrl, getAuthHeaders, parseApiResponse, randomSleep } from '../utils/helpers.js';

// 自定義指標
const errorRate = new Rate('errors');

// 測試配置
const BASE_URL = getBaseUrl();
const VUS = parseInt(__ENV.VUS) || 10;
const DURATION = __ENV.DURATION || '5m';

export const options = {
    stages: [
        { duration: '1m', target: VUS },      // 1 分鐘內逐步增加到目標用戶數
        { duration: DURATION, target: VUS }, // 保持目標用戶數
        { duration: '30s', target: 0 },        // 30 秒內逐步減少到 0
    ],
    thresholds: {
        http_req_duration: ['p(95)<3000', 'p(99)<5000'], // 95% < 3s, 99% < 5s
        http_req_failed: ['rate<0.05'],                   // 錯誤率 < 5%
        errors: ['rate<0.1'],                              // 自定義錯誤率 < 10%
    },
};

export default function () {
    // 場景 1: 瀏覽商品（不需要認證）
    const productResponse = http.get(`${BASE_URL}/Product/GetProductList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductList' },
    });
    
    const productCheck = check(productResponse, {
        '商品列表請求成功': (r) => r.status === 200,
        '響應時間 < 3s': (r) => r.timings.duration < 3000,
        '響應包含數據': (r) => {
            const result = parseApiResponse(r);
            return result.success && result.data !== null;
        },
    });
    
    if (!productCheck) {
        errorRate.add(1);
    }
    
    randomSleep(1, 3);
    
    // 場景 2: 獲取商品基本信息
    const productBasicResponse = http.get(`${BASE_URL}/Product/GetProductBasicInfoList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductBasicInfoList' },
    });
    
    check(productBasicResponse, {
        '商品基本信息請求成功': (r) => r.status === 200,
    });
    
    randomSleep(1, 2);
    
    // 場景 3: 獲取訂單列表（需要認證）
    // 注意: 實際測試時需要有效的認證 token
    const orderResponse = http.get(`${BASE_URL}/Order/GetOrders?query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrders' },
    });
    
    const orderCheck = check(orderResponse, {
        '訂單列表請求可處理': (r) => r.status === 200 || r.status === 401,
    });
    
    if (!orderCheck) {
        errorRate.add(1);
    }
    
    randomSleep(2, 4);
    
    // 場景 4: 獲取單個訂單信息（需要認證）
    // 使用隨機的訂單編號進行測試
    const orderCode = `ORD${Date.now()}${Math.floor(Math.random() * 1000)}`;
    const orderInfoResponse = http.get(`${BASE_URL}/Order/GetOrderInfo?recordCode=${orderCode}`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrderInfo' },
    });
    
    check(orderInfoResponse, {
        '訂單信息請求可處理': (r) => r.status === 200 || r.status === 401 || r.status === 404,
    });
    
    randomSleep(1, 3);
}

export function handleSummary(data) {
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}

function textSummary(data, options) {
    const duration = data.metrics.iteration_duration.values;
    const httpReqs = data.metrics.http_reqs.values;
    const httpDuration = data.metrics.http_req_duration.values;
    const errors = data.metrics.errors.values;
    
    return `
    ====================
    負載測試結果摘要
    ====================
    測試持續時間: ${(duration.max / 1000).toFixed(2)}s
    總請求數: ${httpReqs.count}
    請求速率: ${httpReqs.rate.toFixed(2)} req/s
    
    響應時間統計:
      平均: ${httpDuration.avg.toFixed(2)}ms
      最小: ${httpDuration.min.toFixed(2)}ms
      最大: ${httpDuration.max.toFixed(2)}ms
      p95: ${httpDuration['p(95)'].toFixed(2)}ms
      p99: ${httpDuration['p(99)'].toFixed(2)}ms
    
    錯誤統計:
      錯誤率: ${(errors.rate * 100).toFixed(2)}%
      失敗請求: ${data.metrics.http_req_failed.values.rate * 100}%
    ====================
    `;
}

