using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoCcare.Data.Migrations
{
    /// <inheritdoc />
    public partial class BaseTranLimitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTranLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionLimits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Normal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Silver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Golden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTranLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseTranLimits_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseTranLimits_CreatedById",
                table: "BaseTranLimits",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseTranLimits");
        }
    }
}
