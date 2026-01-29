using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoCcare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoamingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roaming",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mb = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sms = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mtc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cbh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lcl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Thuraya = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inmarsat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roaming", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roaming_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roaming_CreatedById",
                table: "Roaming",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roaming");
        }
    }
}
