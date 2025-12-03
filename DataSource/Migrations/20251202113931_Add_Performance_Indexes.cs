using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Add_Performance_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6282), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6282) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6284), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6285) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6301) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6302), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6302) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6303), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6304) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6305), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6305) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6306), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6306) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6307), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6308) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6309), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6309) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6310), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6212), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6212) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6214), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6214) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6215), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6216) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6217), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6217) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6219), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6219) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6221) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6222), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6222) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6224), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6224) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6225), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6226) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6227), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6227) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6229), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6229) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6230), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6231) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6232), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6232) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6233), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6234) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6235), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6235) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6236), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6237) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6238), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6238) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6239), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6241), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6241) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6242), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6243) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6245), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6245) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6246), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6247) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6248), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6248) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6086), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6088) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6091) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6092), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6092) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6094), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6094) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6095), new DateTime(2025, 12, 2, 11, 39, 31, 177, DateTimeKind.Utc).AddTicks(6096) });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RecordCode",
                table: "Orders",
                column: "RecordCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_RecordCode",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Status",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7774), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7775) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7777), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7777) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7803), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7804) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7804), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7805) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7806), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7806) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7807), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7807) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7808), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7808) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7809), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7809) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7811), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7811) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7694), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7694) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7696), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7696) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7697), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7698) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7699), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7699) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7701) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7702), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7702) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7703), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7704), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7705) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7706), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7706) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7707), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7707) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7708), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7709) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7711), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7712) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7713), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7713) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7714), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7715), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7716) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7717), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7717) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7718), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7718) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7721), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7721) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7722), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7723) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7724), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7724) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7725), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7725) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7726), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7727) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7512), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7516) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7517), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7518) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7519), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7519) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7520), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7521), new DateTime(2025, 11, 23, 15, 51, 15, 516, DateTimeKind.Utc).AddTicks(7522) });
        }
    }
}
