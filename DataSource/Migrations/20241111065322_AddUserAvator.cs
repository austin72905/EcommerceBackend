using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAvator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6886), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6887) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6889), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6889) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6890), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6891) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6892), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6892) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6893), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6893) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6895), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6895) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6896), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6896) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6898), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6898) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6899), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6899) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6900), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6901) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6902), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6902) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6903), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6904) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6905), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6905) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6906), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6906) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6908), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6908) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6909), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6909) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6910), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6911) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6912), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6912) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6913), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6915), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6915) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6916), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6916) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6917), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6918) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6919), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6919) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6920), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6921) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6750), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6759) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6761), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6761) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6763), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6763) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6765), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6765) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6766), new DateTime(2024, 11, 11, 14, 53, 22, 477, DateTimeKind.Local).AddTicks(6767) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Users");

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
    }
}
