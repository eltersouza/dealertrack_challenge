using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dealertrack.UI.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateSequence(
                name: "saleseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "salestatus",
                schema: "ordering",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Id = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salestatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SaleStatusId = table.Column<int>(nullable: false),
                    DealNumber = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: false),
                    DealershipName = table.Column<string>(nullable: false),
                    Vehicle = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sales_salestatus_SaleStatusId",
                        column: x => x.SaleStatusId,
                        principalSchema: "ordering",
                        principalTable: "salestatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "ordering",
                table: "salestatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Paid" });

            migrationBuilder.InsertData(
                schema: "ordering",
                table: "salestatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Pending" });

            migrationBuilder.InsertData(
                schema: "ordering",
                table: "salestatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Cancelled" });

            migrationBuilder.CreateIndex(
                name: "IX_sales_SaleStatusId",
                schema: "ordering",
                table: "sales",
                column: "SaleStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sales",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "salestatus",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "saleseq",
                schema: "ordering");
        }
    }
}
