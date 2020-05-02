using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class Bootlefish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeneralInventory",
                table: "Users",
                type: "LONGTEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralInventory",
                table: "Users");
        }
    }
}
