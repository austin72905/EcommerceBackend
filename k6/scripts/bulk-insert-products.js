/**
 * 批量插入商品測試腳本 (Bulk Insert Products Test)
 * 
 * 目的: 一次性插入 100 件隨機商品到資料庫
 * 執行方式:
 *   k6 run k6/scripts/bulk-insert-products.js
 * 
 * 自定義參數:
 *   BASE_URL=http://localhost:5025 PRODUCT_COUNT=100 k6 run k6/scripts/bulk-insert-products.js
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate, Trend } from 'k6/metrics';
import { getBaseUrl, getAuthHeaders } from '../utils/helpers.js';

// 自定義指標
const errorRate = new Rate('errors');
const responseTimeTrend = new Trend('response_time_trend');
const successRate = new Rate('success');

// 測試配置
const BASE_URL = getBaseUrl();
const PRODUCT_COUNT = parseInt(__ENV.PRODUCT_COUNT) || 100;

// 隨機資料生成器
const colors = ['黑', '白', '灰', '藍', '紅', '綠', '米', '咖啡', '粉', '紫'];
const sizes = ['S', 'M', 'L', 'XL', 'XXL'];
const materials = [
    '聚酯纖維,聚氨酯纖維',
    '純棉,彈性纖維',
    '羊毛,聚酯纖維',
    '絲質,聚酯纖維',
    '亞麻,棉',
    '尼龍,彈性纖維'
];
const kindNames = ['clothes', 'accessories', 'shoes'];
const tagNames = [
    't-shirt', 'shirt', 'jeans', 'shorts', 'windcoat', 
    'knitting', 'accessories', 'new-arrival', 'limit-time-offer'
];

// 隨機商品名稱前綴
const productNamePrefixes = [
    '時尚', '經典', '優雅', '休閒', '運動', '商務', '街頭', '復古', 
    '現代', '簡約', '奢華', '舒適', '潮流', '個性', '清新'
];
const productNameSuffixes = [
    'T恤', '襯衫', '外套', '大衣', '夾克', '褲子', '短褲', '裙子',
    '連帽衫', '毛衣', '背心', '西裝', '風衣', '羽絨服', '牛仔褲'
];

/**
 * 生成隨機商品名稱
 */
function generateProductName() {
    const prefix = productNamePrefixes[Math.floor(Math.random() * productNamePrefixes.length)];
    const suffix = productNameSuffixes[Math.floor(Math.random() * productNameSuffixes.length)];
    const number = Math.floor(Math.random() * 1000) + 1;
    return `${prefix}${suffix}${number}`;
}

/**
 * 生成隨機商品變體
 */
function generateVariants() {
    const variantCount = Math.floor(Math.random() * 3) + 2; // 2-4 個變體
    const variants = [];
    
    for (let i = 0; i < variantCount; i++) {
        const color = colors[Math.floor(Math.random() * colors.length)];
        const size = sizes[Math.floor(Math.random() * sizes.length)];
        const stock = Math.floor(Math.random() * 50) + 10; // 10-60 庫存
        const price = Math.floor(Math.random() * 500) + 100; // 100-600 價格
        const sku = `${color.toUpperCase()}-${size}-${Date.now()}-${i}`;
        
        variants.push({
            color: color,
            sizeValue: size,
            stock: stock,
            sku: sku,
            price: price
        });
    }
    
    return variants;
}

/**
 * 生成隨機商品資料
 */
function generateProductData(index) {
    const productName = generateProductName();
    const material = materials[Math.floor(Math.random() * materials.length)];
    const howToWash = '洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。';
    const features = `這是一款${productName}，採用${material}材質，舒適透氣，適合日常穿著。`;
    const coverImg = `https://ponggoodbf.com/img/product${(index % 5) + 1}.jpg`;
    
    // 生成 3-5 張圖片
    const images = [];
    for (let i = 0; i < Math.floor(Math.random() * 3) + 3; i++) {
        images.push(`https://ponggoodbf.com/img/product${(index + i) % 5 + 1}.jpg`);
    }
    
    // 隨機選擇 1-2 個 Kind
    const selectedKinds = [];
    const kindCount = Math.floor(Math.random() * 2) + 1;
    const shuffledKinds = [...kindNames].sort(() => 0.5 - Math.random());
    for (let i = 0; i < kindCount && i < shuffledKinds.length; i++) {
        selectedKinds.push(shuffledKinds[i]);
    }
    
    // 隨機選擇 2-4 個 Tag
    const selectedTags = [];
    const tagCount = Math.floor(Math.random() * 3) + 2;
    const shuffledTags = [...tagNames].sort(() => 0.5 - Math.random());
    for (let i = 0; i < tagCount && i < shuffledTags.length; i++) {
        selectedTags.push(shuffledTags[i]);
    }
    
    return {
        title: productName,
        material: material,
        howToWash: howToWash,
        features: features,
        coverImg: coverImg,
        images: images,
        kindNames: selectedKinds,
        tagNames: selectedTags,
        variants: generateVariants()
    };
}

export const options = {
    vus: 1, // 單線程執行，避免並發問題
    iterations: PRODUCT_COUNT,
    thresholds: {
        http_req_duration: ['p(95)<5000'],
        http_req_failed: ['rate<0.05'],
        errors: ['rate<0.05'],
        success: ['rate>0.95'],
    },
    summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(50)', 'p(95)', 'p(99)'],
};

export default function () {
    // 生成隨機商品資料
    const productData = generateProductData(__ITER);
    
    // 發送 POST 請求
    const response = http.post(
        `${BASE_URL}/Product/AddNewProduct`,
        JSON.stringify(productData),
        {
            headers: {
                ...getAuthHeaders(),
                'Content-Type': 'application/json',
            },
            tags: { name: 'AddNewProduct', test_type: 'bulk_insert' },
        }
    );
    
    // 檢查回應
    const checkResult = check(response, {
        '新增商品請求成功': (r) => r.status === 200,
        '響應時間 < 5s': (r) => r.timings.duration < 5000,
        '響應包含 ProductId': (r) => {
            if (r.status === 200) {
                try {
                    const body = JSON.parse(r.body);
                    return body.code === 200 && body.data && body.data.productId !== undefined && body.data.productId > 0;
                } catch (e) {
                    return false;
                }
            }
            return false;
        },
    });
    
    errorRate.add(!checkResult ? 1 : 0);
    successRate.add(checkResult ? 1 : 0);
    responseTimeTrend.add(response.timings.duration);
    
    // 每次請求間隔 100ms，避免過快
    sleep(0.1);
}

export function handleSummary(data) {
    const duration = data.metrics.iteration_duration?.values || {};
    const httpReqs = data.metrics.http_reqs?.values || {};
    const httpDuration = data.metrics.http_req_duration?.values || {};
    const errors = data.metrics.errors?.values || {};
    const success = data.metrics.success?.values || {};
    
    const safeToFixed = (value, decimals = 2) => {
        if (value === undefined || value === null || isNaN(value)) {
            return 'N/A';
        }
        return value.toFixed(decimals);
    };
    
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
    批量插入商品測試結果摘要
    ====================
    插入商品數量: ${PRODUCT_COUNT}
    總請求數: ${httpReqs.count || 0}
    請求速率: ${safeToFixed(httpReqs.rate)} req/s
    
    整體響應時間統計:
      平均: ${safeToFixed(httpDuration.avg)}ms
      最小: ${safeToFixed(httpDuration.min)}ms
      最大: ${safeToFixed(httpDuration.max)}ms
      p50: ${safeToFixed(p50)}ms
      p95: ${safeToFixed(p95)}ms
      p99: ${safeToFixed(p99)}ms
    
    成功/失敗統計:
      成功率: ${safeToFixed((success.rate || 0) * 100)}%
      錯誤率: ${safeToFixed((errors.rate || 0) * 100)}%
      成功數: ${success.count || 0}
      失敗數: ${errors.count || 0}
    
    測試評估:
      ${success.rate >= 0.95 ? '✓ 批量插入成功率高於 95%' : '✗ 批量插入成功率低於 95%'}
      ${p95 !== null && p95 < 3000 ? '✓ P95 響應時間優秀' : p95 !== null && p95 < 5000 ? '⚠ P95 響應時間可接受' : '✗ P95 響應時間較慢'}
      ${errors.rate < 0.05 ? '✓ 錯誤率在可接受範圍' : '✗ 錯誤率過高'}
    
    建議:
      ${success.rate < 0.95 ? '- 成功率過低，請檢查服務器日誌和資料庫連接' : ''}
      ${p95 !== null && p95 > 3000 ? '- P95 響應時間較慢，建議檢查資料庫寫入效能' : ''}
      ${errors.rate > 0.05 ? '- 錯誤率過高，建議檢查 API 參數驗證和資料庫約束' : ''}
      ${success.rate >= 0.95 && p95 !== null && p95 < 3000 ? '- 批量插入效能表現良好' : ''}
    ====================
    `;
}

