using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class oofoof : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentBattlefish",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "FishType",
                table: "Battlefish",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBattlefish",
                table: "Users");

            migrationBuilder.AlterColumn<byte>(
                name: "FishType",
                table: "Battlefish",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
