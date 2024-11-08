using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class ModifyShipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "Shipments",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentStatus",
                table: "Shipments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shipments",
                newName: "ShipmentId");

            migrationBuilder.AlterColumn<string>(
                name: "ShipmentStatus",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1025), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1026) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1027), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1028) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1029), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1029) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1031), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1031) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1032), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1033) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1034), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1034) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1119), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1119) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1121), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1121) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1122), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1123) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1124), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1124) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1125), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1126) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1127), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1127) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1128), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1128) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1130), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1131), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1131) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1132), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1133) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1134), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1134) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1135), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1136) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1137), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1137) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1138), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1139) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1140), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1141), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1142) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1143), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1143) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1144), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(1144) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(860), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(869) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(871), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(871) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(873), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(873) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(874), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(875) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(876), new DateTime(2024, 11, 6, 22, 16, 49, 13, DateTimeKind.Local).AddTicks(876) });
        }
    }
}
