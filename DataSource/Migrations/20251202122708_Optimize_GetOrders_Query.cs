using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Optimize_GetOrders_Query : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UpdatedAt",
                table: "Orders",
                column: "UpdatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_UpdatedAt",
                table: "Orders");

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
        }
    }
}
