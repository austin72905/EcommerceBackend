using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
