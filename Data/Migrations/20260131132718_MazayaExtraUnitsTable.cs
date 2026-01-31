using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoCcare.Data.Migrations
{
    /// <inheritdoc />
    public partial class MazayaExtraUnitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MazayaExtraUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraUnits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceVatEx = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceVatIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazayaExtraUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MazayaExtraUnits_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MazayaExtraUnits_CreatedById",
                table: "MazayaExtraUnits",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MazayaExtraUnits");
        }
    }
}
