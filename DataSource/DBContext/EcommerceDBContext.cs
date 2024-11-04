﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataSource.DBContext
{
    public class EcommerceDBContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductKindTag> ProductKindTags { get; set; }
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


            //  設置外鍵關係

            // ProductKindTag 與 Product、Kind、Tag 表的關係
            modelBuilder.Entity<ProductKindTag>()
                .HasOne(pkt => pkt.Product)
                .WithMany(p => p.ProductKindTags)
                .HasForeignKey(pkt => pkt.ProductId);

            modelBuilder.Entity<ProductKindTag>()
                .HasOne(pkt => pkt.Kind)
                .WithMany(k => k.ProductKindTags)
                .HasForeignKey(pkt => pkt.KindId);

            modelBuilder.Entity<ProductKindTag>()
                .HasOne(pkt => pkt.Tag)
                .WithMany(t => t.ProductKindTags)
                .HasForeignKey(pkt => pkt.TagId);

            // ProductDiscount 與 Product、Discount 表的關係
            modelBuilder.Entity<ProductDiscount>()
                .HasOne(pd => pd.Product)
                .WithMany(p => p.ProductDiscounts)
                .HasForeignKey(pd => pd.ProductId);

            modelBuilder.Entity<ProductDiscount>()
                .HasOne(pd => pd.Discount)
                .WithMany(d => d.ProductDiscounts)
                .HasForeignKey(pd => pd.DiscountId);

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
                    Price = 150,
                    Stock = 60,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "http://localhost:9000/coat1.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    Title = "紫色格紋大衣",
                    Price = 598,
                    Stock = 5,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "http://localhost:9000/coat4.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 3,
                    Title = "超質感綠色皮衣",
                    Price = 179,
                    Stock = 18,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "http://localhost:9000/coat3.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 4,
                    Title = "海島風情黑色短袖襯衫",
                    Price = 100,
                    Stock = 60,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "http://localhost:9000/coat2.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 5,
                    Title = "帥氣單寧",
                    Price = 799,
                    Stock = 60,
                    HowToWash = "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features = "其實我也不知道要說什麼...a",
                    CoverImg = "http://localhost:9000/coat5.jpg",
                    Material = "聚酯纖維, 聚氨酯纖維",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );



            // ProductImages 種子數據
            modelBuilder.Entity<ProductImage>().HasData(
                // Images for Product Id 1
                new ProductImage { Id = 1, ProductId = 1, ImageUrl = "http://localhost:9000/coat1.jpg" },
                new ProductImage { Id = 2, ProductId = 1, ImageUrl = "http://localhost:9000/coat2.jpg" },
                new ProductImage { Id = 3, ProductId = 1, ImageUrl = "http://localhost:9000/coat3.jpg" },
                new ProductImage { Id = 4, ProductId = 1, ImageUrl = "http://localhost:9000/coat4.jpg" },
                new ProductImage { Id = 5, ProductId = 1, ImageUrl = "http://localhost:9000/coat5.jpg" },

                // Images for Product Id 2
                new ProductImage { Id = 6, ProductId = 2, ImageUrl = "http://localhost:9000/coat2.jpg" },
                new ProductImage { Id = 7, ProductId = 2, ImageUrl = "http://localhost:9000/coat3.jpg" },
                new ProductImage { Id = 8, ProductId = 2, ImageUrl = "http://localhost:9000/coat4.jpg" },
                new ProductImage { Id = 9, ProductId = 2, ImageUrl = "http://localhost:9000/coat5.jpg" },
                new ProductImage { Id = 10, ProductId = 2, ImageUrl = "http://localhost:9000/coat1.jpg" },

                // Images for Product Id 3
                new ProductImage { Id = 11, ProductId = 3, ImageUrl = "http://localhost:9000/coat3.jpg" },
                new ProductImage { Id = 12, ProductId = 3, ImageUrl = "http://localhost:9000/coat5.jpg" },
                new ProductImage { Id = 13, ProductId = 3, ImageUrl = "http://localhost:9000/coat4.jpg" },
                new ProductImage { Id = 14, ProductId = 3, ImageUrl = "http://localhost:9000/coat2.jpg" },
                new ProductImage { Id = 15, ProductId = 3, ImageUrl = "http://localhost:9000/coat1.jpg" },

                // Images for Product Id 3
                new ProductImage { Id = 16, ProductId = 4, ImageUrl = "http://localhost:9000/coat3.jpg" },
                new ProductImage { Id = 17, ProductId = 4, ImageUrl = "http://localhost:9000/coat5.jpg" },
                new ProductImage { Id = 18, ProductId = 4, ImageUrl = "http://localhost:9000/coat4.jpg" },
                new ProductImage { Id = 19, ProductId = 4, ImageUrl = "http://localhost:9000/coat2.jpg" },
                new ProductImage { Id = 20, ProductId = 4, ImageUrl = "http://localhost:9000/coat1.jpg" },

                // Images for Product Id 3
                new ProductImage { Id = 21, ProductId = 5, ImageUrl = "http://localhost:9000/coat3.jpg" },
                new ProductImage { Id = 22, ProductId = 5, ImageUrl = "http://localhost:9000/coat5.jpg" },
                new ProductImage { Id = 23, ProductId = 5, ImageUrl = "http://localhost:9000/coat4.jpg" },
                new ProductImage { Id = 24, ProductId = 5, ImageUrl = "http://localhost:9000/coat2.jpg" },
                new ProductImage { Id = 25, ProductId = 5, ImageUrl = "http://localhost:9000/coat1.jpg" }
            );

        }
    }
}