using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoCcare.Data.Migrations
{
    /// <inheritdoc />
    public partial class MazayaCostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MazayaCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SimPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceVatInclude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazayaCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MazayaCost_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MazayaCost_CreatedById",
                table: "MazayaCost",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MazayaCost");
        }
    }
}
