using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountAmount = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    HowToWash = table.Column<string>(type: "text", nullable: false),
                    Features = table.Column<string>(type: "text", nullable: false),
                    CoverImg = table.Column<string>(type: "text", nullable: false),
                    Material = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SizeValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MerchantId = table.Column<string>(type: "text", nullable: false),
                    SecretKey = table.Column<string>(type: "text", nullable: false),
                    HashIV = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    GoogleId = table.Column<string>(type: "text", nullable: true),
                    NickName = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
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
                name: "ProductKinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    KindId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductKinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductKinds_Kinds_KindId",
                        column: x => x.KindId,
                        principalTable: "Kinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductKinds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    SizeId = table.Column<int>(type: "integer", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    VariantPrice = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RecipientName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    RecieveWay = table.Column<string>(type: "text", nullable: false),
                    RecieveStore = table.Column<string>(type: "text", nullable: true),
                    AddressLine = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "ProductVariantDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VariantId = table.Column<int>(type: "integer", nullable: false),
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    ProductVariantId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RecordCode = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: true),
                    Receiver = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    RecieveWay = table.Column<string>(type: "text", nullable: false),
                    RecieveStore = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    OrderPrice = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PayWay = table.Column<int>(type: "integer", nullable: false),
                    ShippingPrice = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductVariantId = table.Column<int>(type: "integer", nullable: false),
                    ProductPrice = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    StepStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    TenantConfigId = table.Column<int>(type: "integer", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentStatus = table.Column<byte>(type: "smallint", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ShipmentStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "DiscountAmount", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7774), 100, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7775) },
                    { 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7777), 199, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7777) }
                });

            migrationBuilder.InsertData(
                table: "Kinds",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "clothes" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CoverImg", "CreatedAt", "Features", "HowToWash", "Material", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "https://ponggoodbf.com/img/coat1.jpg", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7512), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", "超時尚流蘇几皮外套", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7516) },
                    { 2, "https://ponggoodbf.com/img/coat4.jpg", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7517), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", "紫色格紋大衣", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7518) },
                    { 3, "https://ponggoodbf.com/img/coat3.jpg", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7519), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", "超質感綠色皮衣", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7519) },
                    { 4, "https://ponggoodbf.com/img/coat2.jpg", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7520), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", "海島風情黑色短袖襯衫", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7520) },
                    { 5, "https://ponggoodbf.com/img/coat5.jpg", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7521), "其實我也不知道要說什麼...a", "洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。", "聚酯纖維, 聚氨酯纖維", "帥氣單寧", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7522) }
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
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "t-shirt" },
                    { 2, "shirt" },
                    { 3, "jeans" },
                    { 4, "shorts" },
                    { 5, "windcoat" },
                    { 6, "knitting" },
                    { 7, "accessories" },
                    { 8, "new-arrival" },
                    { 9, "limit-time-offer" }
                });

            migrationBuilder.InsertData(
                table: "TenantConfigs",
                columns: new[] { "Id", "HashIV", "MerchantId", "SecretKey" },
                values: new object[] { 1, "EkRm7iFT261dpevs", "3002607", "pwFHCqoQZGmho4w6" });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "https://ponggoodbf.com/img/coat1.jpg", 1 },
                    { 2, "https://ponggoodbf.com/img/coat2.jpg", 1 },
                    { 3, "https://ponggoodbf.com/img/coat3.jpg", 1 },
                    { 4, "https://ponggoodbf.com/img/coat4.jpg", 1 },
                    { 5, "https://ponggoodbf.com/img/coat5.jpg", 1 },
                    { 6, "https://ponggoodbf.com/img/coat2.jpg", 2 },
                    { 7, "https://ponggoodbf.com/img/coat3.jpg", 2 },
                    { 8, "https://ponggoodbf.com/img/coat4.jpg", 2 },
                    { 9, "https://ponggoodbf.com/img/coat5.jpg", 2 },
                    { 10, "https://ponggoodbf.com/img/coat1.jpg", 2 },
                    { 11, "https://ponggoodbf.com/img/coat3.jpg", 3 },
                    { 12, "https://ponggoodbf.com/img/coat5.jpg", 3 },
                    { 13, "https://ponggoodbf.com/img/coat4.jpg", 3 },
                    { 14, "https://ponggoodbf.com/img/coat2.jpg", 3 },
                    { 15, "https://ponggoodbf.com/img/coat1.jpg", 3 },
                    { 16, "https://ponggoodbf.com/img/coat3.jpg", 4 },
                    { 17, "https://ponggoodbf.com/img/coat5.jpg", 4 },
                    { 18, "https://ponggoodbf.com/img/coat4.jpg", 4 },
                    { 19, "https://ponggoodbf.com/img/coat2.jpg", 4 },
                    { 20, "https://ponggoodbf.com/img/coat1.jpg", 4 },
                    { 21, "https://ponggoodbf.com/img/coat3.jpg", 5 },
                    { 22, "https://ponggoodbf.com/img/coat5.jpg", 5 },
                    { 23, "https://ponggoodbf.com/img/coat4.jpg", 5 },
                    { 24, "https://ponggoodbf.com/img/coat2.jpg", 5 },
                    { 25, "https://ponggoodbf.com/img/coat1.jpg", 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductTags",
                columns: new[] { "Id", "ProductId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 4 },
                    { 3, 2, 2 },
                    { 4, 3, 3 },
                    { 5, 3, 5 },
                    { 6, 3, 5 },
                    { 7, 4, 6 },
                    { 8, 5, 8 },
                    { 9, 5, 8 },
                    { 10, 5, 9 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "CreatedAt", "ProductId", "SKU", "SizeId", "Stock", "UpdatedAt", "VariantPrice" },
                values: new object[,]
                {
                    { 1, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7694), 1, "BLACK-S", 1, 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7694), 99 },
                    { 2, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7696), 1, "BLACK-L", 3, 16, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7696), 283 },
                    { 3, "米", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7697), 1, "WHEAT-L", 3, 3, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7698), 150 },
                    { 4, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7699), 1, "BROWN-M", 2, 17, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7699), 199 },
                    { 5, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7700), 1, "BROWN-L", 3, 20, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7701), 211 },
                    { 6, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7702), 2, "BLACK-S", 1, 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7702), 99 },
                    { 7, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7703), 2, "BLACK-L", 3, 16, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7703), 283 },
                    { 8, "米", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7704), 2, "WHEAT-L", 3, 3, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7705), 150 },
                    { 9, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7706), 2, "BROWN-M", 2, 17, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7706), 199 },
                    { 10, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7707), 3, "BLACK-S", 1, 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7707), 99 },
                    { 11, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7708), 3, "BLACK-L", 3, 16, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7709), 283 },
                    { 12, "米", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7710), 3, "WHEAT-L", 3, 3, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7710), 150 },
                    { 13, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7711), 3, "BROWN-M", 2, 17, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7712), 199 },
                    { 14, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7713), 3, "BROWN-L", 3, 20, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7713), 211 },
                    { 15, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7714), 4, "BLACK-S", 1, 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7714), 99 },
                    { 16, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7715), 4, "BLACK-L", 3, 16, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7716), 283 },
                    { 17, "米", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7717), 4, "WHEAT-L", 3, 3, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7717), 150 },
                    { 18, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7718), 4, "BROWN-M", 2, 17, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7718), 199 },
                    { 19, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7720), 4, "BROWN-L", 3, 20, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7720), 211 },
                    { 20, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7721), 5, "BLACK-S", 1, 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7721), 99 },
                    { 21, "黑", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7722), 5, "BLACK-L", 3, 16, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7723), 283 },
                    { 22, "米", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7724), 5, "WHEAT-L", 3, 3, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7724), 150 },
                    { 23, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7725), 5, "BROWN-M", 2, 17, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7725), 199 },
                    { 24, "咖啡", new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7726), 5, "BROWN-L", 3, 20, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7727), 211 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariantDiscounts",
                columns: new[] { "Id", "CreatedAt", "DiscountId", "UpdatedAt", "VariantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7803), 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7804), 2 },
                    { 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7804), 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7805), 4 },
                    { 3, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7806), 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7806), 8 },
                    { 4, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7807), 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7807), 11 },
                    { 5, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7808), 1, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7808), 12 },
                    { 6, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7809), 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7809), 19 },
                    { 7, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7810), 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7810), 22 },
                    { 8, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7811), 2, new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7811), 24 }
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
                column: "UserId",
                unique: true);

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
                name: "IX_Kinds_Name",
                table: "Kinds",
                column: "Name",
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
                name: "IX_ProductKinds_KindId",
                table: "ProductKinds",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKinds_ProductId",
                table: "ProductKinds",
                column: "ProductId");

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
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");

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
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

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
                name: "ProductKinds");

            migrationBuilder.DropTable(
                name: "ProductMaterials");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "ProductVariantDiscounts");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "TenantConfigs");

            migrationBuilder.DropTable(
                name: "Kinds");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Orders");

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
