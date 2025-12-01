/**
 * 峰值測試 (Spike Test)
 * 
 * 目的: 測試系統處理突然增加的負載（如促銷活動、突發流量）
 * 負載: 從 10 個用戶突然增加到 100 個用戶
 * 
 * 執行方式:
 *   k6 run k6/scripts/spike-test.js
 * 
 * 警告: 此測試會對系統造成極大壓力，請在測試環境執行
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate, Counter } from 'k6/metrics';
import { getBaseUrl, getAuthHeaders, parseApiResponse } from '../utils/helpers.js';

// 自定義指標
const errorRate = new Rate('errors');
const spikeCounter = new Counter('spike_requests');

// 測試配置
const BASE_URL = getBaseUrl();
const SPIKE_VUS = parseInt(__ENV.SPIKE_VUS) || 100;

export const options = {
    stages: [
        { duration: '1m', target: 10 },        // 1 分鐘內增加到 10 個用戶（正常負載）
        { duration: '30s', target: SPIKE_VUS }, // 30 秒內突然增加到峰值（模擬突發流量）
        { duration: '1m', target: SPIKE_VUS },  // 保持峰值 1 分鐘
        { duration: '30s', target: 10 },       // 30 秒內快速減少到正常負載
        { duration: '1m', target: 10 },        // 保持正常負載 1 分鐘
        { duration: '30s', target: 0 },         // 30 秒內減少到 0
    ],
    thresholds: {
        // 峰值測試時，我們主要關注系統是否崩潰，而不是響應時間
        http_req_duration: ['p(95)<10000'],    // 95% < 10s（峰值時放寬標準）
        http_req_failed: ['rate<0.2'],         // 錯誤率 < 20%（峰值時允許較高錯誤率）
        errors: ['rate<0.25'],                 // 自定義錯誤率 < 25%
    },
};

export default function () {
    spikeCounter.add(1);
    
    // 場景 1: 突發流量 - 大量用戶同時瀏覽商品
    const productResponse = http.get(`${BASE_URL}/Product/GetProductList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductList', test_type: 'spike' },
    });
    
    const productCheck = check(productResponse, {
        '商品列表請求可處理': (r) => {
            // 在峰值測試中，我們接受各種狀態碼（除了 500）
            return r.status !== 500;
        },
        '響應時間 < 15s': (r) => r.timings.duration < 15000,
    });
    
    if (!productCheck) {
        errorRate.add(1);
    }
    
    // 峰值時減少 sleep 時間，模擬高頻請求
    sleep(0.5);
    
    // 場景 2: 突發流量 - 大量用戶查詢訂單
    const orderResponse = http.get(`${BASE_URL}/Order/GetOrders?query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrders', test_type: 'spike' },
    });
    
    const orderCheck = check(orderResponse, {
        '訂單列表請求可處理': (r) => {
            // 接受 200, 401, 429 (Too Many Requests), 503
            return [200, 401, 429, 503].includes(r.status);
        },
    });
    
    if (!orderCheck && orderResponse.status === 500) {
        errorRate.add(1);
    }
    
    sleep(0.5);
    
    // 場景 3: 突發流量 - 大量用戶查詢訂單詳情
    const orderCode = `ORD${Date.now()}${Math.floor(Math.random() * 1000)}`;
    const orderInfoResponse = http.get(`${BASE_URL}/Order/GetOrderInfo?recordCode=${orderCode}`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetOrderInfo', test_type: 'spike' },
    });
    
    check(orderInfoResponse, {
        '訂單信息請求可處理': (r) => {
            return [200, 401, 404, 429, 503].includes(r.status);
        },
    });
    
    sleep(0.5);
    
    // 場景 4: 突發流量 - 大量用戶獲取商品基本信息
    const productBasicResponse = http.get(`${BASE_URL}/Product/GetProductBasicInfoList?tag=all&kind=all&query=`, {
        headers: getAuthHeaders(),
        tags: { name: 'GetProductBasicInfoList', test_type: 'spike' },
    });
    
    check(productBasicResponse, {
        '商品基本信息請求可處理': (r) => r.status !== 500,
    });
    
    sleep(1);
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
    const spikeRequests = data.metrics.spike_requests.values;
    
    return `
    ====================
    峰值測試結果摘要
    ====================
    最大並發用戶數: ${vus.max}
    峰值請求數: ${spikeRequests.count}
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
    
    系統峰值處理能力評估:
      ${errors.rate < 0.2 ? '✓ 系統在峰值負載下基本穩定' : '✗ 系統在峰值負載下出現較多錯誤'}
      ${httpDuration['p(95)'] < 10000 ? '✓ 峰值時響應時間可接受' : '✗ 峰值時響應時間較慢'}
      ${vus.max >= SPIKE_VUS ? '✓ 系統能夠處理峰值負載' : '✗ 系統未能達到預期峰值負載'}
    
    建議:
      ${errors.rate > 0.15 ? '- 考慮增加服務器資源或實施限流機制' : ''}
      ${httpDuration['p(95)'] > 5000 ? '- 優化響應時間，考慮使用緩存或負載均衡' : ''}
    ====================
    `;
}

