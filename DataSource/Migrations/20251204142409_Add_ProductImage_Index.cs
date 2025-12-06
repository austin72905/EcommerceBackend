using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProductImage_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(95), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(95) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(97), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(97) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(111), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(111) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(112), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(113) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(114), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(114) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(115), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(115) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(116), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(116) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(117), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(117) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(118), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(119) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(119), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9997), new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9998) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(29), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(30) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(31), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(31) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(33), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(33) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(34), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(34) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(36), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(36) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(37), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(38) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(40), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(40) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(41), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(41) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(42), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(43) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(44), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(44) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(45), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(45) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(47), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(47) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(48), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(48) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(49), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(50) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(51), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(51) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(52), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(52) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(53), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(54) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(55), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(55) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(56), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(56) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(57), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(59) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(60), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(60) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(61), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(61) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(62), new DateTime(2025, 12, 4, 14, 24, 9, 440, DateTimeKind.Utc).AddTicks(63) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9885), new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9888) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9890), new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9890) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9891), new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9892) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9893), new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9893) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9894), new DateTime(2025, 12, 4, 14, 24, 9, 439, DateTimeKind.Utc).AddTicks(9894) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2179), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2181), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2182) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2209), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2209) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2210), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2211) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2211), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2212) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2212), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2213) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2213), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2214) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2214), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2215) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2215), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2216) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2217), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2217) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2095), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2095) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2097), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2097) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2099), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2099) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2100), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2101), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2102) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2103), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2103) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2104), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2105) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2106), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2106) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2107), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2107) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2108), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2109) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2110), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2111), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2112) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2113), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2114), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2114) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2115), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2116) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2117), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2117) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2118), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2118) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2120), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2120) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2121), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2121) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2122), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2123) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2125), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2125) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2126), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2126) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2128), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2128) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2129), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(2129) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1900), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1905) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1906), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1906) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1907), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1908) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1909), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1909) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1910), new DateTime(2025, 12, 2, 12, 54, 25, 253, DateTimeKind.Utc).AddTicks(1911) });
        }
    }
}
