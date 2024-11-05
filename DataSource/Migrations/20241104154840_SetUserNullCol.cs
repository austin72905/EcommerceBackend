using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class SetUserNullCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9013), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9013) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9015), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9016) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9017), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9017) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9019), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9020), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9021) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9022), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9022) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9023), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9024) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9025), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9025) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9026), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9026) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9028), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9028) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9029), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9029) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9030), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9031) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9032), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9032) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9033), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9034) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9035), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9035) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9036), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9037) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9038), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9038) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9039), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9040) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9041), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9041) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9042), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9042) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9044), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9044) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9045), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9045) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9046), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9047) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9048), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(9048) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8888), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8898) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8900), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8902), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8902) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8904), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8904) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8905), new DateTime(2024, 11, 4, 23, 48, 40, 630, DateTimeKind.Local).AddTicks(8906) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(138), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(139) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(141), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(142) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(143), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(144) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(145), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(145) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(146), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(147) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(148), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(148) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(150), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(150) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(151), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(151) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(153), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(153) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(154), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(155) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(156), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(156) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(157), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(158) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(159), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(159) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(160), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(161) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(162), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(162) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(163), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(164) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(165), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(165) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(166), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(166) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(167), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(168) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(169), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(169) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(170), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(171) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(172), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(172) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(173), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(173) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(174), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(175) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 766, DateTimeKind.Local).AddTicks(9989), new DateTime(2024, 11, 4, 21, 14, 20, 766, DateTimeKind.Local).AddTicks(9998) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(2), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(2) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(4), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(4) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(5), new DateTime(2024, 11, 4, 21, 14, 20, 767, DateTimeKind.Local).AddTicks(6) });
        }
    }
}
