using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataSource.DBContext
{
    public class EcommerceDBContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<ProductKind> ProductKinds { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantDiscount> ProductVariantDiscounts { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserShipAddress> UserShipAddresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStep> OrderSteps { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<TenantConfig> TenantConfigs { get; set; }


        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options) : base(options)
        {

        }


        // set seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // 索引
            // 在 FavoriteProduct 表中為 UserId 和 ProductId 設置唯一索引
            modelBuilder.Entity<FavoriteProduct>()
               .HasIndex(fp => new { fp.UserId, fp.ProductId })
               .IsUnique();

            // Product 表的 Title 欄位索引
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Title);

            // User 表的 Email 和 Username 欄位索引
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Email 通常是唯一的
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(); // Username 通常是唯一的

            // Order 表的 UserId 外鍵索引
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserId);

            // OrderProduct 表的 OrderId 和 ProductVariantId 外鍵索引
            modelBuilder.Entity<OrderProduct>()
                .HasIndex(op => op.OrderId);
            modelBuilder.Entity<OrderProduct>()
                .HasIndex(op => op.ProductVariantId);


            // 設置 UserId 在 Cart 表中唯一
            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.UserId)
                .IsUnique();
            // CartItem 表的 CartId 和 ProductVariantId 外鍵索引
            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => ci.CartId);
            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => ci.ProductVariantId);

            // ProductDiscount 表的 ProductId 和 DiscountId 外鍵索引
            modelBuilder.Entity<ProductDiscount>()
                .HasIndex(pd => pd.ProductId);
            modelBuilder.Entity<ProductDiscount>()
                .HasIndex(pd => pd.DiscountId);

            // ProductVariantDiscount 表的 VariantId 和 DiscountId 外鍵索引
            modelBuilder.Entity<ProductVariantDiscount>()
                .HasIndex(pvd => pvd.VariantId);
            modelBuilder.Entity<ProductVariantDiscount>()
                .HasIndex(pvd => pvd.DiscountId);

            // OrderStep 表的 OrderId 外鍵索引
            modelBuilder.Entity<OrderStep>()
                .HasIndex(os => os.OrderId);

            // Kind 表的 Name 欄位索引
            modelBuilder.Entity<Kind>()
                .HasIndex(k => k.Name)
                .IsUnique();

            // Tag 表的 Name  欄位索引
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();



            //  設置外鍵關係

            // cart 與 user 關係
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User) // Cart 的 User 导航属性
                .WithMany()          // User 没有 Carts 导航属性
                .HasForeignKey(c => c.UserId);

            // 配置 Cart 和 CartItem 的關係
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // 配置 ProductKind 表
            modelBuilder.Entity<ProductKind>()
                .HasKey(pk => pk.Id);

            modelBuilder.Entity<ProductKind>()
                .HasOne(pk => pk.Product)
                .WithMany(p => p.ProductKinds)
                .HasForeignKey(pk => pk.ProductId);

            modelBuilder.Entity<ProductKind>()
                .HasOne(pk => pk.Kind)
                .WithMany(k => k.ProductKinds)
                .HasForeignKey(pk => pk.KindId);

            // 配置 ProductTag 表
            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => pt.Id);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);


            // ProductDiscount 與 Product、Discount 表的關係
            //modelBuilder.Entity<ProductDiscount>()
            //    .HasOne(pd => pd.Product)
            //    .WithMany(p => p.ProductDiscounts)
            //    .HasForeignKey(pd => pd.ProductId);

            //modelBuilder.Entity<ProductDiscount>()
            //    .HasOne(pd => pd.Discount)
            //    .WithMany(d => d.ProductDiscounts)
            //    .HasForeignKey(pd => pd.DiscountId);

            // ProductVariantDiscount 與 ProductVariant、Discount 表的關係
            modelBuilder.Entity<ProductVariantDiscount>()
                .HasOne(pvd => pvd.ProductVariant)
                .WithMany(pv => pv.ProductVariantDiscounts)
                .HasForeignKey(pvd => pvd.VariantId);

            modelBuilder.Entity<ProductVariantDiscount>()
                .HasOne(pvd => pvd.Discount)
                .WithMany(d => d.ProductVariantDiscounts)
                .HasForeignKey(pvd => pvd.DiscountId);

            // ProductMaterial 與 Product、Material 表的關係
            modelBuilder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMaterials)
                .HasForeignKey(pm => pm.ProductId);

            modelBuilder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Material)
                .WithMany(m => m.ProductMaterials)
                .HasForeignKey(pm => pm.MaterialId);

            // ProductImage 與 Product 表的關係
            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId);


            // FavoriteProduct 與 User、Product 表的關係
            modelBuilder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId);

            modelBuilder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.Product)
                .WithMany(p => p.FavoriteProducts)
                .HasForeignKey(fp => fp.ProductId);

            // OrderProduct 與 Order、ProductVariant 表的關係
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.ProductVariant)
                .WithMany(pv => pv.OrderProducts)
                .HasForeignKey(op => op.ProductVariantId);

            // Payment 與 Order、TenantConfig 表的關係
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.TenantConfig)
                .WithMany(tc => tc.Payments)
                .HasForeignKey(p => p.TenantConfigId);


            // 種子資料
            // Product 種子數據
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "超時尚流蘇几皮外套",
                    //Price = 150,
                    //Stock = 60,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "https://ponggoodbf.com/img/coat1.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Title = "紫色格紋大衣",
                    //Price = 598,
                    //Stock = 5,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "https://ponggoodbf.com/img/coat4.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Title = "超質感綠色皮衣",
                    //Price = 179,
                    //Stock = 18,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "https://ponggoodbf.com/img/coat3.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 4,
                    Title = "海島風情黑色短袖襯衫",
                    //Price = 100,
                    //Stock = 60,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "https://ponggoodbf.com/img/coat2.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 5,
                    Title = "帥氣單寧",
                    //Price = 799,
                    //Stock = 60,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "https://ponggoodbf.com/img/coat5.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // Size 種子數據
            modelBuilder.Entity<Domain.Entities.Size>().HasData(
                new Size { Id = 1, SizeValue = "S" },
                new Size { Id = 2, SizeValue = "M" },
                new Size { Id = 3, SizeValue = "L" }
            );

            // ProductVariant 種子數據
            modelBuilder.Entity<ProductVariant>().HasData(
                new ProductVariant
                {
                    Id = 1,
                    ProductId = 1,
                    Color = "黑",
                    SizeId = 1, // 對應 S
                    SKU = "BLACK-S",
                    Stock = 2,
                    VariantPrice = 99,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 2,
                    ProductId = 1,
                    Color = "黑",
                    SizeId = 3, // 對應 L
                    SKU = "BLACK-L",
                    Stock = 16,
                    VariantPrice = 283,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 3,
                    ProductId = 1,
                    Color = "米",
                    SizeId = 3, // 對應 L
                    SKU = "WHEAT-L",
                    Stock = 3,
                    VariantPrice = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 4,
                    ProductId = 1,
                    Color = "咖啡",
                    SizeId = 2, // 對應 M
                    SKU = "BROWN-M",
                    Stock = 17,
                    VariantPrice = 199,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 5,
                    ProductId = 1,
                    Color = "咖啡",
                    SizeId = 3, // 對應 L
                    SKU = "BROWN-L",
                    Stock = 20,
                    VariantPrice = 211,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 6,
                    ProductId = 2,
                    Color = "黑",
                    SizeId = 1,
                    SKU = "BLACK-S",
                    Stock = 2,
                    VariantPrice = 99,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 7,
                    ProductId = 2,
                    Color = "黑",
                    SizeId = 3, // 對應 L
                    SKU = "BLACK-L",
                    Stock = 16,
                    VariantPrice = 283,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 8,
                    ProductId = 2,
                    Color = "米",
                    SizeId = 3,
                    SKU = "WHEAT-L",
                    Stock = 3,
                    VariantPrice = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 9,
                    ProductId = 2,
                    Color = "咖啡",
                    SizeId = 2, // 對應 M
                    SKU = "BROWN-M",
                    Stock = 17,
                    VariantPrice = 199,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 10,
                    ProductId = 3,
                    Color = "黑",
                    SizeId = 1, // 對應 S
                    SKU = "BLACK-S",
                    Stock = 2,
                    VariantPrice = 99,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 11,
                    ProductId = 3,
                    Color = "黑",
                    SizeId = 3, // 對應 L
                    SKU = "BLACK-L",
                    Stock = 16,
                    VariantPrice = 283,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 12,
                    ProductId = 3,
                    Color = "米",
                    SizeId = 3, // 對應 L
                    SKU = "WHEAT-L",
                    Stock = 3,
                    VariantPrice = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 13,
                    ProductId = 3,
                    Color = "咖啡",
                    SizeId = 2, // 對應 M
                    SKU = "BROWN-M",
                    Stock = 17,
                    VariantPrice = 199,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 14,
                    ProductId = 3,
                    Color = "咖啡",
                    SizeId = 3, // 對應 L
                    SKU = "BROWN-L",
                    Stock = 20,
                    VariantPrice = 211,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 15,
                    ProductId = 4,
                    Color = "黑",
                    SizeId = 1, // 對應 S
                    SKU = "BLACK-S",
                    Stock = 2,
                    VariantPrice = 99,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 16,
                    ProductId = 4,
                    Color = "黑",
                    SizeId = 3, // 對應 L
                    SKU = "BLACK-L",
                    Stock = 16,
                    VariantPrice = 283,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 17,
                    ProductId = 4,
                    Color = "米",
                    SizeId = 3, // 對應 L
                    SKU = "WHEAT-L",
                    Stock = 3,
                    VariantPrice = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 18,
                    ProductId = 4,
                    Color = "咖啡",
                    SizeId = 2, // 對應 M
                    SKU = "BROWN-M",
                    Stock = 17,
                    VariantPrice = 199,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 19,
                    ProductId = 4,
                    Color = "咖啡",
                    SizeId = 3, // 對應 L
                    SKU = "BROWN-L",
                    Stock = 20,
                    VariantPrice = 211,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 20,
                    ProductId = 5,
                    Color = "黑",
                    SizeId = 1, // 對應 S
                    SKU = "BLACK-S",
                    Stock = 2,
                    VariantPrice = 99,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 21,
                    ProductId = 5,
                    Color = "黑",
                    SizeId = 3, // 對應 L
                    SKU = "BLACK-L",
                    Stock = 16,
                    VariantPrice = 283,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 22,
                    ProductId = 5,
                    Color = "米",
                    SizeId = 3, // 對應 L
                    SKU = "WHEAT-L",
                    Stock = 3,
                    VariantPrice = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 23,
                    ProductId = 5,
                    Color = "咖啡",
                    SizeId = 2, // 對應 M
                    SKU = "BROWN-M",
                    Stock = 17,
                    VariantPrice = 199,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariant
                {
                    Id = 24,
                    ProductId = 5,
                    Color = "咖啡",
                    SizeId = 3, // 對應 L
                    SKU = "BROWN-L",
                    Stock = 20,
                    VariantPrice = 211,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            //  Discount 種子數據
            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    Id = 1,
                    DiscountAmount = 100,
                    StartDate = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2025, 12, 31), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Discount
                {
                    Id = 2,
                    DiscountAmount = 199,
                    StartDate = DateTime.SpecifyKind(new DateTime(2024, 2, 1), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2025, 12, 31), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // 設定 ProductVariantDiscount 種子資料
            modelBuilder.Entity<ProductVariantDiscount>().HasData(
                new ProductVariantDiscount
                {
                    Id = 1,
                    VariantId = 2, 
                    DiscountId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariantDiscount
                {
                    Id = 2,
                    VariantId = 4, 
                    DiscountId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariantDiscount
                {
                    Id = 3,
                    VariantId = 8,
                    DiscountId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariantDiscount
                {
                    Id = 4,
                    VariantId = 11,
                    DiscountId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariantDiscount
                {
                    Id = 5,
                    VariantId = 12,
                    DiscountId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                 new ProductVariantDiscount
                 {
                     Id = 6,
                     VariantId = 19,
                     DiscountId = 2,
                     CreatedAt = DateTime.UtcNow,
                     UpdatedAt = DateTime.UtcNow
                 },
                new ProductVariantDiscount
                {
                    Id = 7,
                    VariantId = 22,
                    DiscountId = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductVariantDiscount
                {
                    Id = 8,
                    VariantId = 24,
                    DiscountId = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );


            // ProductImages 種子數據
            modelBuilder.Entity<ProductImage>().HasData(
                // Images for Product Id 1
                new ProductImage { Id = 1, ProductId = 1, ImageUrl = "https://ponggoodbf.com/img/coat1.jpg" },
                new ProductImage { Id = 2, ProductId = 1, ImageUrl = "https://ponggoodbf.com/img/coat2.jpg" },
                new ProductImage { Id = 3, ProductId = 1, ImageUrl = "https://ponggoodbf.com/img/coat3.jpg" },
                new ProductImage { Id = 4, ProductId = 1, ImageUrl = "https://ponggoodbf.com/img/coat4.jpg" },
                new ProductImage { Id = 5, ProductId = 1, ImageUrl = "https://ponggoodbf.com/img/coat5.jpg" },

                // Images for Product Id 2
                new ProductImage { Id = 6, ProductId = 2, ImageUrl = "https://ponggoodbf.com/img/coat2.jpg" },
                new ProductImage { Id = 7, ProductId = 2, ImageUrl = "https://ponggoodbf.com/img/coat3.jpg" },
                new ProductImage { Id = 8, ProductId = 2, ImageUrl = "https://ponggoodbf.com/img/coat4.jpg" },
                new ProductImage { Id = 9, ProductId = 2, ImageUrl = "https://ponggoodbf.com/img/coat5.jpg" },
                new ProductImage { Id = 10, ProductId = 2, ImageUrl = "https://ponggoodbf.com/img/coat1.jpg" },

                // Images for Product Id 3
                new ProductImage { Id = 11, ProductId = 3, ImageUrl = "https://ponggoodbf.com/img/coat3.jpg" },
                new ProductImage { Id = 12, ProductId = 3, ImageUrl = "https://ponggoodbf.com/img/coat5.jpg" },
                new ProductImage { Id = 13, ProductId = 3, ImageUrl = "https://ponggoodbf.com/img/coat4.jpg" },
                new ProductImage { Id = 14, ProductId = 3, ImageUrl = "https://ponggoodbf.com/img/coat2.jpg" },
                new ProductImage { Id = 15, ProductId = 3, ImageUrl = "https://ponggoodbf.com/img/coat1.jpg" },

                // Images for Product Id 3
                new ProductImage { Id = 16, ProductId = 4, ImageUrl = "https://ponggoodbf.com/img/coat3.jpg" },
                new ProductImage { Id = 17, ProductId = 4, ImageUrl = "https://ponggoodbf.com/img/coat5.jpg" },
                new ProductImage { Id = 18, ProductId = 4, ImageUrl = "https://ponggoodbf.com/img/coat4.jpg" },
                new ProductImage { Id = 19, ProductId = 4, ImageUrl = "https://ponggoodbf.com/img/coat2.jpg" },
                new ProductImage { Id = 20, ProductId = 4, ImageUrl = "https://ponggoodbf.com/img/coat1.jpg" },

                // Images for Product Id 3
                new ProductImage { Id = 21, ProductId = 5, ImageUrl = "https://ponggoodbf.com/img/coat3.jpg" },
                new ProductImage { Id = 22, ProductId = 5, ImageUrl = "https://ponggoodbf.com/img/coat5.jpg" },
                new ProductImage { Id = 23, ProductId = 5, ImageUrl = "https://ponggoodbf.com/img/coat4.jpg" },
                new ProductImage { Id = 24, ProductId = 5, ImageUrl = "https://ponggoodbf.com/img/coat2.jpg" },
                new ProductImage { Id = 25, ProductId = 5, ImageUrl = "https://ponggoodbf.com/img/coat1.jpg" }
            );


            // Kind 種子數據
            modelBuilder.Entity<Kind>().HasData(
                new Kind { Id = 1, Name = "clothes" }
            );

            // Tag 種子數據，與 Kind 進行關聯
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "t-shirt" },
                new Tag { Id = 2, Name = "shirt" },
                new Tag { Id = 3, Name = "jeans" },
                new Tag { Id = 4, Name = "shorts" },
                new Tag { Id = 5, Name = "windcoat" },
                new Tag { Id = 6, Name = "knitting" },
                new Tag { Id = 7, Name = "accessories" },
                new Tag { Id = 8, Name = "new-arrival" },
                new Tag { Id = 9, Name = "limit-time-offer" }
            );


            // ProductTag 種子數據
            modelBuilder.Entity<ProductTag>().HasData(
                new ProductTag { Id = 1, ProductId = 1, TagId = 1 },
                new ProductTag { Id = 2, ProductId = 1, TagId = 4 },
                new ProductTag { Id = 3, ProductId = 2, TagId = 2 },
                new ProductTag { Id = 4, ProductId = 3, TagId = 3 },
                new ProductTag { Id = 5, ProductId = 3, TagId = 5 },
                new ProductTag { Id = 6, ProductId = 3, TagId = 5 },
                new ProductTag { Id = 7, ProductId = 4, TagId = 6 },
                new ProductTag { Id = 8, ProductId = 5, TagId = 8 },
                new ProductTag { Id = 9, ProductId = 5, TagId = 8 },
                new ProductTag { Id = 10, ProductId = 5, TagId = 9 }
            );

            // TenantConfig 種子資料 ECPAY 公開測試密鑰
            modelBuilder.Entity<TenantConfig>().HasData(
                new TenantConfig
                {
                    Id = 1,
                    MerchantId = "3002607",
                    SecretKey = "pwFHCqoQZGmho4w6",
                    HashIV = "EkRm7iFT261dpevs",
                }

            );

        }
    }
}
