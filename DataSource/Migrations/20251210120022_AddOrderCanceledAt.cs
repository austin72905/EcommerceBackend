using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderCanceledAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3262), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3262) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3264), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3265) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3279), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3281), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3281) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3282), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3283) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3284), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3284) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3285), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3285) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3286), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3286) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3287), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3288), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3289) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3186), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3187) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3188), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3189) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3191) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3192), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3193) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3194), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3194) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3196), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3196) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3198), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3198) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3199), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3202), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3202) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3204), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3204) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3205), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3206) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3207), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3208) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3209), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3209) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3211) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3212), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3213) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3214), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3214) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3215), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3216) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3217), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3217) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3219), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3219) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3221) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3222), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3222) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3226), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3226) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3228), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3228) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3229), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3022), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3024) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3026), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3027) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3028), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3029) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3031) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3032), new DateTime(2025, 12, 10, 12, 0, 22, 427, DateTimeKind.Utc).AddTicks(3032) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledAt",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(223), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(223) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(225), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(225) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(237), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(238) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(264), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(265) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(265), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(266) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(267), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(267) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(268), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(268) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(269), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(269) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(270), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(271), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(271) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(154), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(155) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(159), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(159) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(161), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(161) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(162), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(163) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(164), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(164) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(165), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(166) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(168), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(169) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(170), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(170) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(171), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(171) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(172), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(173) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(174), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(174) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(175), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(176) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(177), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(177) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(178), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(178) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(179), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(180) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(182), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(182) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(183), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(183) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(185), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(185) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(186), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(186) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(187), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(188) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(189), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(189) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(190), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(190) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(191), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(192) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(24), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(26) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(28), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(28) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(30), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(30) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(31), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(32) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(33), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(33) });
        }
    }
}
