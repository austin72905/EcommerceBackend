using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class InitProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiscountAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    HowToWash = table.Column<string>(type: "TEXT", nullable: false),
                    Features = table.Column<string>(type: "TEXT", nullable: false),
                    CoverImg = table.Column<string>(type: "TEXT", nullable: false),
                    Material = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SizeValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MerchantId = table.Column<string>(type: "TEXT", nullable: false),
                    SecretKey = table.Column<string>(type: "TEXT", nullable: false),
                    HashIV = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    GoogleId = table.Column<string>(type: "TEXT", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KindId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Kinds_KindId",
                        column: x => x.KindId,
                        principalTable: "Kinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDiscounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMaterials_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    SizeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    SKU = table.Column<string>(type: "TEXT", nullable: false),
                    VariantPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserShipAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipientName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    AddressLine = table.Column<string>(type: "TEXT", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShipAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserShipAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductKindTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    KindId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductKindTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductKindTags_Kinds_KindId",
                        column: x => x.KindId,
                        principalTable: "Kinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductKindTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductKindTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VariantId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariantDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantDiscounts_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductVariantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordCode = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: true),
                    Receiver = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress = table.Column<string>(type: "TEXT", nullable: false),
                    OrderPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    PayWay = table.Column<int>(type: "INTEGER", nullable: false),
                    ShippingPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_UserShipAddresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "UserShipAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductVariantId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    StepStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSteps_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantConfigId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    TransactionId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_TenantConfigs_TenantConfigId",
                        column: x => x.TenantConfigId,
                        principalTable: "TenantConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShipmentStatus = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CoverImg", "CreatedAt", "Features", "HowToWash", "Material", "Price", "Stock", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "http://localhost:9000/coat1.jpg", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2784), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", 150, 60, "超時尚流蘇几皮外套", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2793) },
                    { 2, "http://localhost:9000/coat4.jpg", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2795), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", 598, 5, "紫色格紋大衣", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2795) },
                    { 3, "http://localhost:9000/coat3.jpg", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2797), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", 179, 18, "超質感綠色皮衣", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2797) },
                    { 4, "http://localhost:9000/coat2.jpg", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2799), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", 100, 60, "海島風情黑色短袖襯衫", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2799) },
                    { 5, "http://localhost:9000/coat5.jpg", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2800), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", 799, 60, "帥氣單寧", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2801) }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "SizeValue" },
                values: new object[,]
                {
                    { 1, "S" },
                    { 2, "M" },
                    { 3, "L" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "http://localhost:9000/coat1.jpg", 1 },
                    { 2, "http://localhost:9000/coat2.jpg", 1 },
                    { 3, "http://localhost:9000/coat3.jpg", 1 },
                    { 4, "http://localhost:9000/coat4.jpg", 1 },
                    { 5, "http://localhost:9000/coat5.jpg", 1 },
                    { 6, "http://localhost:9000/coat2.jpg", 2 },
                    { 7, "http://localhost:9000/coat3.jpg", 2 },
                    { 8, "http://localhost:9000/coat4.jpg", 2 },
                    { 9, "http://localhost:9000/coat5.jpg", 2 },
                    { 10, "http://localhost:9000/coat1.jpg", 2 },
                    { 11, "http://localhost:9000/coat3.jpg", 3 },
                    { 12, "http://localhost:9000/coat5.jpg", 3 },
                    { 13, "http://localhost:9000/coat4.jpg", 3 },
                    { 14, "http://localhost:9000/coat2.jpg", 3 },
                    { 15, "http://localhost:9000/coat1.jpg", 3 },
                    { 16, "http://localhost:9000/coat3.jpg", 4 },
                    { 17, "http://localhost:9000/coat5.jpg", 4 },
                    { 18, "http://localhost:9000/coat4.jpg", 4 },
                    { 19, "http://localhost:9000/coat2.jpg", 4 },
                    { 20, "http://localhost:9000/coat1.jpg", 4 },
                    { 21, "http://localhost:9000/coat3.jpg", 5 },
                    { 22, "http://localhost:9000/coat5.jpg", 5 },
                    { 23, "http://localhost:9000/coat4.jpg", 5 },
                    { 24, "http://localhost:9000/coat2.jpg", 5 },
                    { 25, "http://localhost:9000/coat1.jpg", 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "CreatedAt", "ProductId", "SKU", "SizeId", "Stock", "UpdatedAt", "VariantPrice" },
                values: new object[,]
                {
                    { 1, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2931), 1, "BLACK-S", 1, 2, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2931), 99 },
                    { 2, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2933), 1, "BLACK-L", 3, 16, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2933), 283 },
                    { 3, "米", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2935), 1, "WHEAT-L", 3, 3, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2935), 150 },
                    { 4, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2936), 1, "BROWN-M", 2, 17, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2937), 199 },
                    { 5, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2938), 1, "BROWN-L", 3, 20, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2938), 211 },
                    { 6, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2939), 2, "BLACK-S", 1, 2, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2940), 99 },
                    { 7, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2941), 2, "BLACK-L", 3, 16, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2941), 283 },
                    { 8, "米", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2942), 2, "WHEAT-L", 3, 3, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2942), 150 },
                    { 9, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2944), 2, "BROWN-M", 2, 17, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2944), 199 },
                    { 10, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2945), 3, "BLACK-S", 1, 2, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2946), 99 },
                    { 11, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2947), 3, "BLACK-L", 3, 16, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2947), 283 },
                    { 12, "米", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2948), 3, "WHEAT-L", 3, 3, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2948), 150 },
                    { 13, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2950), 3, "BROWN-M", 2, 17, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2950), 199 },
                    { 14, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2951), 3, "BROWN-L", 3, 20, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2951), 211 },
                    { 15, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2952), 4, "BLACK-S", 1, 2, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2953), 99 },
                    { 16, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2954), 4, "BLACK-L", 3, 16, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2954), 283 },
                    { 17, "米", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2955), 4, "WHEAT-L", 3, 3, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2956), 150 },
                    { 18, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2957), 4, "BROWN-M", 2, 17, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2957), 199 },
                    { 19, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2958), 4, "BROWN-L", 3, 20, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2958), 211 },
                    { 20, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2960), 5, "BLACK-S", 1, 2, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2960), 99 },
                    { 21, "黑", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2961), 5, "BLACK-L", 3, 16, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2961), 283 },
                    { 22, "米", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2962), 5, "WHEAT-L", 3, 3, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2963), 150 },
                    { 23, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2964), 5, "BROWN-M", 2, 17, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2964), 199 },
                    { 24, "咖啡", new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2965), 5, "BROWN-L", 3, 20, new DateTime(2024, 11, 4, 18, 21, 22, 421, DateTimeKind.Local).AddTicks(2965), 211 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductVariantId",
                table: "CartItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_ProductId",
                table: "FavoriteProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_UserId_ProductId",
                table: "FavoriteProducts",
                columns: new[] { "UserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductVariantId",
                table: "OrderProducts",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSteps_OrderId",
                table: "OrderSteps",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TenantConfigId",
                table: "Payments",
                column: "TenantConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_DiscountId",
                table: "ProductDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProductId",
                table: "ProductDiscounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKindTags_KindId",
                table: "ProductKindTags",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKindTags_ProductId",
                table: "ProductKindTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKindTags_TagId",
                table: "ProductKindTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_MaterialId",
                table: "ProductMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Title",
                table: "Products",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantDiscounts_DiscountId",
                table: "ProductVariantDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantDiscounts_VariantId",
                table: "ProductVariantDiscounts",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SizeId",
                table: "ProductVariants",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_KindId",
                table: "Tags",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserShipAddresses_UserId",
                table: "UserShipAddresses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "OrderSteps");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductDiscounts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductKindTags");

            migrationBuilder.DropTable(
                name: "ProductMaterials");

            migrationBuilder.DropTable(
                name: "ProductVariantDiscounts");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "TenantConfigs");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Kinds");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "UserShipAddresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
