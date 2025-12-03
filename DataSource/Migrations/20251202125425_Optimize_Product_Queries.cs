using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Optimize_Product_Queries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7030), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7030) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7032), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7033) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7050), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7051) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7052), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7052) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7053), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7053) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7054), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7055) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7056), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7056) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7057), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7057) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7058), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7058) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7059), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(7060) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6924), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6925) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6927), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6927) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6928), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6930), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6930) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6932), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6932) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6933), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6934) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6935), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6936) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6937), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6938) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6939), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6939) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6940), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6942), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6942) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6944), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6944) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6945), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6946) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6947), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6947) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6948), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6949) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6950), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6950) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6951), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6952) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6953), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6953) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6954), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6955) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6956), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6956) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6957), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6958) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6959), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6959) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6960), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6960) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6962), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6962) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6691), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6693) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6695), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6695) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6697), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6697) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6699), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6699) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6700), new DateTime(2025, 12, 2, 12, 27, 7, 871, DateTimeKind.Utc).AddTicks(6701) });
        }
    }
}
