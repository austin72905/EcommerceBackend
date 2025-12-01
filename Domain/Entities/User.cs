using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// 用戶聚合根 - 富領域模型
    /// 
    /// 聚合邊界：
    /// - User（聚合根）
    /// - UserShipAddress（聚合內實體）
    /// - FavoriteProduct（聚合內實體）
    /// 
    /// 不變性（Invariants）：
    /// 1. Email 必須唯一且不可為空
    /// 2. 帳號密碼用戶必須有 PasswordHash
    /// 3. Google 登入用戶必須有 GoogleId，且不能有 PasswordHash
    /// 4. Google 登入用戶無法更新密碼
    /// 5. 用戶角色必須是有效的（預設為 "user"）
    /// 6. 同一商品不能重複添加到收藏清單
    /// </summary>
    public class User : IAggregateRoot
    {
        // 私有構造函數
        private User()
        {
            FavoriteProducts = new List<FavoriteProduct>();
            UserShipAddresses = new List<UserShipAddress>();
            Role = "user"; // 預設角色
        }

        // 工廠方法：創建帳號密碼用戶（使用值對象進行驗證）
        public static User CreateWithPassword(string email, string username, string passwordHash)
        {
            // 使用 Email 值對象進行驗證
            var emailValue = ValueObjects.Email.Create(email);

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("用戶名不能為空", nameof(username));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("密碼雜湊不能為空", nameof(passwordHash));

            return new User
            {
                Email = emailValue.Value, // 存儲為字串（EF Core 映射）
                Username = username,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };
        }

        // 工廠方法：創建 Google 登入用戶（使用值對象進行驗證）
        public static User CreateWithGoogle(string email, string googleId, string nickName = null, string picture = null)
        {
            // 使用 Email 值對象進行驗證
            var emailValue = ValueObjects.Email.Create(email);

            if (string.IsNullOrWhiteSpace(googleId))
                throw new ArgumentException("Google ID 不能為空", nameof(googleId));

            return new User
            {
                Email = emailValue.Value, // 存儲為字串（EF Core 映射）
                GoogleId = googleId,
                NickName = nickName,
                Picture = picture,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };
        }

        // 只讀屬性或私有 setter
        public int Id { get; private set; }
        public string? Username { get; private set; }
        public string Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? GoogleId { get; private set; }
        public string? NickName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Gender { get; private set; }
        public string? Picture { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastLogin { get; private set; }

        // ============ 值對象屬性（用於業務邏輯） ============

        /// <summary>
        /// Email 值對象
        /// </summary>
        public ValueObjects.Email EmailValue => ValueObjects.Email.Create(Email);

        // 導航屬性
        public ICollection<FavoriteProduct> FavoriteProducts { get; private set; }
        public ICollection<UserShipAddress> UserShipAddresses { get; private set; }

        // ============ 業務邏輯方法 ============

        /// <summary>
        /// 更新個人資料
        /// </summary>
        public void UpdateProfile(string nickName = null, string phoneNumber = null, string gender = null, DateTime? birthday = null)
        {
            if (!string.IsNullOrWhiteSpace(nickName))
                NickName = nickName;

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumber = phoneNumber;

            if (!string.IsNullOrWhiteSpace(gender))
                Gender = gender;

            if (birthday.HasValue)
                Birthday = birthday;

            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 更新密碼
        /// </summary>
        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("密碼雜湊不能為空", nameof(newPasswordHash));

            if (!string.IsNullOrWhiteSpace(GoogleId))
                throw new InvalidOperationException("Google 登入用戶無法更新密碼");

            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 更新頭像
        /// </summary>
        public void UpdatePicture(string pictureUrl)
        {
            if (string.IsNullOrWhiteSpace(pictureUrl))
                throw new ArgumentException("圖片 URL 不能為空", nameof(pictureUrl));

            Picture = pictureUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 記錄登入時間
        /// </summary>
        public void RecordLogin()
        {
            LastLogin = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 添加收貨地址
        /// </summary>
        public void AddShippingAddress(UserShipAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            UserShipAddresses.Add(address);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 移除收貨地址
        /// </summary>
        public void RemoveShippingAddress(int addressId)
        {
            var address = UserShipAddresses.FirstOrDefault(a => a.Id == addressId);
            
            if (address == null)
                throw new InvalidOperationException("地址不存在");

            UserShipAddresses.Remove(address);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 添加收藏商品
        /// </summary>
        public void AddFavoriteProduct(int productId)
        {
            if (FavoriteProducts.Any(fp => fp.ProductId == productId))
                return; // 已經收藏，不重複添加

            var favorite = new FavoriteProduct
            {
                UserId = Id,
                ProductId = productId
            };

            FavoriteProducts.Add(favorite);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 移除收藏商品
        /// </summary>
        public void RemoveFavoriteProduct(int productId)
        {
            var favorite = FavoriteProducts.FirstOrDefault(fp => fp.ProductId == productId);
            
            if (favorite != null)
            {
                FavoriteProducts.Remove(favorite);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// 檢查是否為管理員
        /// </summary>
        public bool IsAdmin()
        {
            return Role?.ToLower() == "admin";
        }

        /// <summary>
        /// 設置為管理員
        /// </summary>
        public void SetAsAdmin()
        {
            Role = "admin";
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
