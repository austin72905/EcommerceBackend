using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Add_PayStatus_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8036), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8036) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8038), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8038) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8051), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8052) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8053), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8053) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8054), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8055) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8055), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8056) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8057), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8057) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8058), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8058) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8059), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8059) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7931), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7931) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7934), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7935) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7936), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7936) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7937), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7938) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7939), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7939) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7979), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7981), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7981) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7983), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7983) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7984), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7984) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7985), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7986) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7987), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7988) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7989), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7991), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7991) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7992), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7993) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7994), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7994) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7995), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7995) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7996), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7997) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7998), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7998) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7999), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7999) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8000), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8001) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8002), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8002) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8003), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8003) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8004), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8005) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8006), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(8006) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7789), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7792) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7794), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7794) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7796), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7796) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7797), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7798) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7799), new DateTime(2025, 12, 5, 9, 16, 9, 194, DateTimeKind.Utc).AddTicks(7799) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantDiscounts_VariantId_DiscountId",
                table: "ProductVariantDiscounts",
                columns: new[] { "VariantId", "DiscountId" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatus",
                table: "Payments",
                column: "PaymentStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariantDiscounts_VariantId_DiscountId",
                table: "ProductVariantDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentStatus",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9787), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9788) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9790), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9805), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9805) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9806), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9807) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9808), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9808) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9809), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9809) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9810), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9811) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9811), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9812) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9812), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9813) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9813), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9814) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9689), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9689) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9691), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9692) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9725), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9725) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9726), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9727) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9728), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9728) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9729), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9730) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9731), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9731) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9732), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9733) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9734), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9734) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9735), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9736) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9737), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9737) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9739) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9740), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9740) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9741), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9741) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9742), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9744), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9744) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9745), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9746) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9748), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9748) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9749), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9749) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9751) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9752), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9752) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9753), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9753) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9754), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9755) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9756), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9756) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9564), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9565) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9567), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9567) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9569), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9569) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9572), new DateTime(2025, 12, 5, 8, 16, 17, 202, DateTimeKind.Utc).AddTicks(9572) });
        }
    }
}
