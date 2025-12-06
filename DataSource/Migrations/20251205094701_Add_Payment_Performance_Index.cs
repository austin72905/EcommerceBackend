using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Add_Payment_Performance_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4487), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4487) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4489), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4501), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4502) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4504), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4505) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4505), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4506) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4507), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4507) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4508), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4508) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4509), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4509) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4510), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4511), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4511) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4398), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4398) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4401), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4402) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4403), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4403) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4405), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4405) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4407), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4407) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4408), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4409) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4411), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4411) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4412), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4413) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4414), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4414) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4415), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4416) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4417), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4417) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4418), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4418) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4442), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4442) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4443), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4444) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4445), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4445) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4446), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4446) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4447), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4448) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4449), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4449) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4451), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4453), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4453) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4454), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4455) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4456), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4456) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4252) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4254), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4254) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4255), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4256) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4257), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4257) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4258), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4259) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
