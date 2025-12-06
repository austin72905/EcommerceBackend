/**
 * 測試用戶註冊腳本 (Test Users Setup Script)
 *
 * 用途: 註冊100個固定的測試用戶，確保資料庫中有這些用戶資料
 * 這些用戶將用於壓力測試腳本
 *
 * 執行方式:
 *   k6 run k6/scripts/setup-test-users.js
 *
 * 自定義參數:
 *   BASE_URL=http://localhost:5025 TEST_PASSWORD=Test123456! k6 run k6/scripts/setup-test-users.js
 */

import http from 'k6/http';
import { check, sleep } from 'k6';
import { getBaseUrl, getAuthHeaders, parseApiResponse } from '../utils/helpers.js';

// ===== 配置 =====
const BASE_URL = getBaseUrl();
const TEST_PASSWORD = __ENV.TEST_PASSWORD || 'Test123456!';
const TOTAL_USERS = 100;

// ===== 生成測試用戶列表 =====
const TEST_USERS = [];
for (let i = 1; i <= TOTAL_USERS; i++) {
    TEST_USERS.push({
        Username: `testuser${i}`,
        Password: TEST_PASSWORD,
        Email: `testuser${i}@test.com`,
        NickName: `測試用戶${i}`,
    });
}

export const options = {
    vus: 1, // 單線程執行，避免並發註冊衝突
    iterations: 1,
    duration: '1m', // 最多執行1分鐘
};

// ===== 統計指標 =====
let successCount = 0;
let alreadyExistsCount = 0;
let failureCount = 0;

export default function () {
    console.log(`\n開始註冊 ${TOTAL_USERS} 個測試用戶...`);
    console.log(`BASE_URL: ${BASE_URL}`);
    console.log(`密碼: ${TEST_PASSWORD}\n`);

    for (let i = 0; i < TEST_USERS.length; i++) {
        const user = TEST_USERS[i];
        const userNum = i + 1;

        const signUpData = {
            Username: user.Username,
            Email: user.Email,
            NickName: user.NickName,
            Password: user.Password,
        };

        const signUpResponse = http.post(
            `${BASE_URL}/User/UserRegister`,
            JSON.stringify(signUpData),
            {
                headers: getAuthHeaders(),
            }
        );

        const signUpCheck = check(signUpResponse, {
            '註冊 HTTP 請求可處理': (r) => [200, 400, 409, 429, 500, 503].includes(r.status),
        });

        if (signUpResponse.status === 200) {
            try {
                const result = parseApiResponse(signUpResponse);
                if (result.success) {
                    successCount++;
                    console.log(`[${userNum}/${TOTAL_USERS}] ✓ 註冊成功: ${user.Username}`);
                } else {
                    // 檢查是否為用戶已存在的錯誤
                    if (result.error && (result.error.includes('已存在') || result.error.includes('exists'))) {
                        alreadyExistsCount++;
                        console.log(`[${userNum}/${TOTAL_USERS}] ⚠ 用戶已存在: ${user.Username}`);
                    } else {
                        failureCount++;
                        console.log(`[${userNum}/${TOTAL_USERS}] ✗ 註冊失敗: ${user.Username} - ${result.error || '未知錯誤'}`);
                    }
                }
            } catch (e) {
                failureCount++;
                console.log(`[${userNum}/${TOTAL_USERS}] ✗ 解析響應失敗: ${user.Username} - ${e.message}`);
            }
        } else if (signUpResponse.status === 409) {
            // 409 通常表示用戶已存在
            alreadyExistsCount++;
            console.log(`[${userNum}/${TOTAL_USERS}] ⚠ 用戶已存在 (409): ${user.Username}`);
        } else {
            failureCount++;
            console.log(`[${userNum}/${TOTAL_USERS}] ✗ 註冊失敗: ${user.Username} - HTTP ${signUpResponse.status}`);
            console.log(`    響應內容: ${signUpResponse.body.substring(0, 200)}`);
        }

        // 稍微延遲，避免請求過快
        if (i < TEST_USERS.length - 1) {
            sleep(0.1); // 100ms 延遲
        }
    }

    // 顯示統計結果
    console.log(`\n========== 註冊結果統計 ==========`);
    console.log(`總用戶數: ${TOTAL_USERS}`);
    console.log(`✓ 成功註冊: ${successCount}`);
    console.log(`⚠ 已存在: ${alreadyExistsCount}`);
    console.log(`✗ 失敗: ${failureCount}`);
    console.log(`===================================\n`);

    // 驗證：嘗試登入幾個用戶確認註冊成功
    console.log('驗證用戶登入...\n');
    let verifySuccess = 0;
    let verifyFailure = 0;

    // 驗證前5個用戶
    for (let i = 0; i < Math.min(5, TEST_USERS.length); i++) {
        const user = TEST_USERS[i];
        const loginData = {
            Username: user.Username,
            Password: user.Password,
        };

        const loginResponse = http.post(
            `${BASE_URL}/User/UserLogin`,
            JSON.stringify(loginData),
            {
                headers: getAuthHeaders(),
            }
        );

        if (loginResponse.status === 200) {
            const result = parseApiResponse(loginResponse);
            if (result.success) {
                verifySuccess++;
                console.log(`✓ 驗證成功: ${user.Username} 可以登入`);
            } else {
                verifyFailure++;
                console.log(`✗ 驗證失敗: ${user.Username} 無法登入 - ${result.error || '未知錯誤'}`);
            }
        } else {
            verifyFailure++;
            console.log(`✗ 驗證失敗: ${user.Username} - HTTP ${loginResponse.status}`);
        }
    }

    console.log(`\n驗證結果: ${verifySuccess} 成功, ${verifyFailure} 失敗\n`);

    if (successCount + alreadyExistsCount === TOTAL_USERS) {
        console.log('✓ 所有用戶都已準備就緒（成功註冊或已存在）');
    } else {
        console.log('⚠ 部分用戶註冊失敗，請檢查錯誤訊息');
    }
}

