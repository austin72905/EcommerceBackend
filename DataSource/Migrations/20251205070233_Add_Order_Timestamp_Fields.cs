using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Add_Order_Timestamp_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickedUpAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaidAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickedUpAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippedAt",
                table: "Orders");

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
    }
}
