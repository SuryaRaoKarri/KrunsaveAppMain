using Microsoft.EntityFrameworkCore.Migrations;

namespace Krunsave.Migrations
{
    public partial class StatusColumnAddToAvailablefood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Availablefoods",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Availablefoods");
        }
    }
}
