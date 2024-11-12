using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class CartUseridUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

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
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3477), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3486) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3489), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3489) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3491), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3491) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3519), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3519) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3521), new DateTime(2024, 11, 13, 0, 54, 37, 602, DateTimeKind.Local).AddTicks(3521) });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

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

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");
        }
    }
}
