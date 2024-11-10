using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAddressEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserShipAddresses");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1830), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1832), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1832) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1834), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1834) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1835), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1836) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1837), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1837) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1838), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1839) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1840), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1841), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1842) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1843), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1843) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1844), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1845) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1846), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1846) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1847), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1848) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1849), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1849) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1850), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1852), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1852) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1853), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1853) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1854), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1855) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1856), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1856) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1857), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1858) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1859), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1859) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1884), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1885) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1886), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1886) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1887), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1888) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1889), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1889) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1693), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1703) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1705), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1705) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1707), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1707) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1708), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1709) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1710), new DateTime(2024, 11, 11, 1, 50, 26, 866, DateTimeKind.Local).AddTicks(1710) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserShipAddresses",
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
    }
}
