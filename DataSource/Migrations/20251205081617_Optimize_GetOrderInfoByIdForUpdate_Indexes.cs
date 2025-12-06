using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Optimize_GetOrderInfoByIdForUpdate_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 添加 UserShipAddresses.Id 索引（雖然主鍵已有索引，但明確添加以確保優化器使用）
            // 注意：PostgreSQL 主鍵自動有索引，但為了確保 JOIN 效能，我們明確添加
            // 實際上主鍵索引已經存在，這裡主要是為了 ProductVariantDiscounts 的複合索引

            // 添加 ProductVariantDiscounts 複合索引（優化 JOIN 查詢效能）
            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantDiscounts_VariantId_DiscountId",
                table: "ProductVariantDiscounts",
                columns: new[] { "VariantId", "DiscountId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 移除 ProductVariantDiscounts 複合索引
            migrationBuilder.DropIndex(
                name: "IX_ProductVariantDiscounts_VariantId_DiscountId",
                table: "ProductVariantDiscounts");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9453), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9454) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9456), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9456) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9472), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9472) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9473), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9473) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9474), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9475) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9476), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9476) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9477), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9477) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9478), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9478) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9479), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9479) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9357), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9359) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9360), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9361) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9362), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9362) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9364), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9364) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9365), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9366) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9370), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9371), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9372) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9373), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9373) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9374), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9374) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9377), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9377) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9378), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9379) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9380), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9380) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9381), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9381) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9385), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9385) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9386), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9386) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9388), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9388) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9389), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9389) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9390), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9391) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9392), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9392) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9393), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9393) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9394), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9395) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9396), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9396) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9397), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9397) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9221), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9223) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9225), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9225) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9226), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9227) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9228), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9228) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9229), new DateTime(2025, 12, 5, 7, 2, 33, 456, DateTimeKind.Utc).AddTicks(9230) });
        }
    }
}
