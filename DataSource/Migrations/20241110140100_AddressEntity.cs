using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class AddressEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecieveStore",
                table: "UserShipAddresses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecieveWay",
                table: "UserShipAddresses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecieveStore",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecieveWay",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6115), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6115) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6118), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6118) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6119), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6121), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6122) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6123), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6123) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6124), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6125) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6126), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6126) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6127), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6128) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6129), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6129) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6130), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6131) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6132), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6132) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6133), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6133) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6135), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6135) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6136), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6136) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6138), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6138) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6139), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6139) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6140), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6141) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6142), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6142) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6143), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6144) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6145), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6145) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6146), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6146) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6148), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6148) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6149), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6149) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6150), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(6151) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5972), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5982) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5984), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5984) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5986), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5987) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5988), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5989) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5990), new DateTime(2024, 11, 10, 22, 1, 0, 754, DateTimeKind.Local).AddTicks(5990) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieveStore",
                table: "UserShipAddresses");

            migrationBuilder.DropColumn(
                name: "RecieveWay",
                table: "UserShipAddresses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RecieveStore",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RecieveWay",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7150), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7150) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7194), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7195) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7196), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7196) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7197), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7198) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7199), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7199) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7201), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7203), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7203) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7204), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7204) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7207), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7207) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7209), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7209) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7210), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7211) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7212), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7212) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7214), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7215) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7216), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7216) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7217), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7217) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7218), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7219) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7220), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7220) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7221), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7222) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7223), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7223) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7224), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7224) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7225), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7226) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7227), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7227) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7228), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7228) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7229), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7024), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7033) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7035), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7036) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7038), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7038) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7039), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7041), new DateTime(2024, 11, 8, 16, 41, 8, 538, DateTimeKind.Local).AddTicks(7042) });
        }
    }
}
