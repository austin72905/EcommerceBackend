using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "DiscountAmount", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3691), 100, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3692) },
                    { 2, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3693), 199, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3694) }
                });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3626), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3626) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3628), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3628) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3630), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3630) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3631), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3632) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3633), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3633) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3634), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3635) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3636), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3636) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3637), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3638) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3639), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3639) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3640), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3641) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3642), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3642) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3643), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3644) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3645), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3645) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3646), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3647) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3648), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3648) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3649), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3650) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3651), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3651) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3652), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3653), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3654) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3655), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3655) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3656), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3657) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3658), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3658) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3659), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3659) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3661), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3661) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3485), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3495) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3497), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3497) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3499), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3499) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3500), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3501), new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3502) });

            migrationBuilder.InsertData(
                table: "ProductVariantDiscounts",
                columns: new[] { "Id", "CreatedAt", "DiscountId", "UpdatedAt", "VariantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3711), 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3711), 2 },
                    { 2, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3712), 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3713), 4 },
                    { 3, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3714), 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3714), 8 },
                    { 4, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3715), 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3715), 11 },
                    { 5, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3716), 1, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3716), 12 },
                    { 6, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3717), 2, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3718), 19 },
                    { 7, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3718), 2, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3719), 22 },
                    { 8, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3719), 2, new DateTime(2024, 11, 14, 18, 36, 41, 386, DateTimeKind.Local).AddTicks(3720), 24 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3651), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3653), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3654) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3655), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3655) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3657), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3657) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3658), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3659) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3660), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3660) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3661), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3662) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3663), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3663) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3664), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3665) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3666), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3666) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3667), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3668) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3669), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3669) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3670), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3671) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3672), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3672) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3673), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3674) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3675), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3675) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3676), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3678), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3678) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3679), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3679) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3681), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3681) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3682), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3682) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3683), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3684) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3685), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3685) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3686), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3686) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3477), 150, 60, new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3486) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3489), 598, 5, new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3489) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3491), 179, 18, new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3491) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3519), 100, 60, new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3519) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3521), 799, 60, new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3521) });
        }
    }
}
