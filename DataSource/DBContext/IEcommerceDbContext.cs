using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataSource.DBContext
{
    /// <summary>
    /// 定義 Ecommerce DbContext 的共同介面，包含所有 DbSet 屬性
    /// 用於讀寫分離時統一存取 DbSet
    /// </summary>
    public interface IEcommerceDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Kind> Kinds { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<ProductTag> ProductTags { get; set; }
        DbSet<ProductKind> ProductKinds { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<ProductDiscount> ProductDiscounts { get; set; }
        DbSet<ProductVariant> ProductVariants { get; set; }
        DbSet<ProductVariantDiscount> ProductVariantDiscounts { get; set; }
        DbSet<Material> Materials { get; set; }
        DbSet<ProductMaterial> ProductMaterials { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Size> Sizes { get; set; }
        DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserShipAddress> UserShipAddresses { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderProduct> OrderProducts { get; set; }
        DbSet<OrderStep> OrderSteps { get; set; }
        DbSet<Shipment> Shipments { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<TenantConfig> TenantConfigs { get; set; }
    }
}

