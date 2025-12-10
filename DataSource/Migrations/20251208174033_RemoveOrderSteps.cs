using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataSource.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrderSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSteps");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(223), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(223) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(225), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(225) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(237), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(238) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(264), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(265) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(265), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(266) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(267), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(267) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(268), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(268) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(269), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(269) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(270), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(271), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(271) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(154), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(155) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(159), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(159) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(161), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(161) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(162), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(163) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(164), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(164) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(165), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(166) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(168), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(169) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(170), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(170) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(171), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(171) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(172), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(173) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(174), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(174) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(175), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(176) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(177), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(177) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(178), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(178) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(179), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(180) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(182), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(182) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(183), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(183) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(185), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(185) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(186), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(186) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(187), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(188) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(189), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(189) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(190), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(190) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(191), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(192) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(24), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(26) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(28), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(28) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(30), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(30) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(31), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(32) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(33), new DateTime(2025, 12, 8, 17, 40, 33, 35, DateTimeKind.Utc).AddTicks(33) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StepStatus = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSteps_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4487), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4487) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4489), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4501), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4502) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4504), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4505) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4505), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4506) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4507), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4507) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4508), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4508) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4509), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4509) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4510), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "ProductVariantDiscounts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4511), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4511) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4398), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4398) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4401), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4402) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4403), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4403) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4405), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4405) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4407), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4407) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4408), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4409) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4411), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4411) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4412), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4413) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4414), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4414) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4415), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4416) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4417), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4417) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4418), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4418) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4442), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4442) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4443), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4444) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4445), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4445) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4446), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4446) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4447), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4448) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4449), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4449) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4451), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4453), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4453) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4454), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4455) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4456), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4456) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4252) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4254), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4254) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4255), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4256) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4257), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4257) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4258), new DateTime(2025, 12, 5, 9, 47, 1, 421, DateTimeKind.Utc).AddTicks(4259) });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSteps_OrderId",
                table: "OrderSteps",
                column: "OrderId");
        }
    }
}
