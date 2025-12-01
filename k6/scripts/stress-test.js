/**
 * 壓力測試 (Stress Test)
 * 
 * 目的: 測試系統在超過正常負載下的表現，找出系統極限
 * 負載: 逐步增加到 50 個虛擬用戶
 * 
 * 執行方式:
 *   k6 run k6/scripts/stress-test.js
 * 
 * 警告: 此測試會對系統造成較大壓力，請在測試環境執行
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate, Trend } from 'k6/metrics';
import { getBaseUrl, getAuthHeaders, parseApiResponse, randomSleep } from '../utils/helpers.js';

// 自定義指標
const errorRate = new Rate('errors');
const responseTimeTrend = new Trend('response_time_trend');

// 測試配置
const BASE_URL = getBaseUrl();
const MAX_VUS = parseInt(__ENV.MAX_VUS) || 50;

export const options = {
    stages: [
        { duration: '2m', target: 10 },   // 2 分鐘內增加到 10 個用戶
        { duration: '3m', target: 25 },   // 3 分鐘內增加到 25 個用戶
        { duration: '5m', target: MAX_VUS }, // 5 分鐘內增加到最大用戶數
        { duration: '5m', target: MAX_VUS }, // 保持最大用戶數 5 分鐘
        { duration: '2m', target: 25 },   // 2 分鐘內減少到 25 個用戶
        { duration: '2m', target: 10 },   // 2 分鐘內減少到 10 個用戶
        { duration: '1m', target: 0 },   // 1 分鐘內減少到 0
    ],
    thresholds: {
        http_req_duration: ['p(95)<5000', 'p(99)<10000'], // 壓力測試時放寬標準
        http_req_failed: ['rate<0.1'],                     // 錯誤率 < 10%
        errors: ['rate<0.15'],                             // 自定義錯誤率 < 15%
    },
};

export default function () {
    // 場景 1: 高頻率瀏覽商品
    const productResponse = http.get(`${BASE_URL}/Product/GetProductList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductList', test_type: 'stress' },
    });
    
    const productCheck = check(productResponse, {
        '商品列表請求成功': (r) => r.status === 200,
        '響應時間 < 10s': (r) => r.timings.duration < 10000,
    });
    
    if (!productCheck) {
        errorRate.add(1);
    }
    
    responseTimeTrend.add(productResponse.timings.duration);
    
    randomSleep(0.5, 2);
    
    // 場景 2: 並發獲取商品基本信息
    const productBasicResponse = http.get(`${BASE_URL}/Product/GetProductBasicInfoList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductBasicInfoList', test_type: 'stress' },
    });
    
    check(productBasicResponse, {
        '商品基本信息請求可處理': (r) => r.status === 200 || r.status === 503,
    });
    
    randomSleep(0.5, 1.5);
    
    // 場景 3: 並發查詢訂單（需要認證）
    const orderResponse = http.get(`${BASE_URL}/Order/GetOrders?query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrders', test_type: 'stress' },
    });
    
    const orderCheck = check(orderResponse, {
        '訂單列表請求可處理': (r) => {
            // 在壓力測試中，我們接受 200, 401, 429 (Too Many Requests), 503
            return [200, 401, 429, 503].includes(r.status);
        },
    });
    
    if (!orderCheck) {
        errorRate.add(1);
    }
    
    randomSleep(1, 3);
    
    // 場景 4: 並發查詢訂單詳情
    const orderCode = `ORD${Date.now()}${Math.floor(Math.random() * 1000)}`;
    const orderInfoResponse = http.get(`${BASE_URL}/Order/GetOrderInfo?recordCode=${orderCode}`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrderInfo', test_type: 'stress' },
    });
    
    check(orderInfoResponse, {
        '訂單信息請求可處理': (r) => {
            return [200, 401, 404, 429, 503].includes(r.status);
        },
    });
    
    randomSleep(1, 2);
}

export function handleSummary(data) {
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}

function textSummary(data, options) {
    const httpReqs = data.metrics.http_reqs.values;
    const httpDuration = data.metrics.http_req_duration.values;
    const errors = data.metrics.errors.values;
    const vus = data.metrics.vus.values;
    
    return `
    ====================
    壓力測試結果摘要
    ====================
    最大並發用戶數: ${vus.max}
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
      失敗請求: ${(data.metrics.http_req_failed.values.rate * 100).toFixed(2)}%
    
    系統狀態評估:
      ${errors.rate < 0.1 ? '✓ 系統在壓力下表現良好' : '✗ 系統在壓力下出現較多錯誤'}
      ${httpDuration['p(95)'] < 5000 ? '✓ 響應時間可接受' : '✗ 響應時間較慢，需要優化'}
    ====================
    `;
}

