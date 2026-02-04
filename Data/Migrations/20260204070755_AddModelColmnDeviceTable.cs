using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoCcare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModelColmnDeviceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Device",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Device");
        }
    }
}
