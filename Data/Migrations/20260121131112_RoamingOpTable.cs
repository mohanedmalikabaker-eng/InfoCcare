using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoCcare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoamingOpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoamingOp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TapCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoamingParName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    McCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    McNtCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoamingServCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateOut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoamingOp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoamingOp_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoamingOp_CreatedById",
                table: "RoamingOp",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoamingOp");
        }
    }
}
