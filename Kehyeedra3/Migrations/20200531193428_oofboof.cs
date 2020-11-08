using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class oofboof : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleFishObject_Users_UserId",
                table: "BattleFishObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BattleFishObject",
                table: "BattleFishObject");

            migrationBuilder.DropIndex(
                name: "IX_BattleFishObject_UserId",
                table: "BattleFishObject");

            migrationBuilder.RenameTable(
                name: "BattleFishObject",
                newName: "Battlefish");

            migrationBuilder.AlterColumn<ulong>(
                name: "UserId",
                table: "Battlefish",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint unsigned",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Battlefish",
                table: "Battlefish",
                column: "FishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Battlefish",
                table: "Battlefish");

            migrationBuilder.RenameTable(
                name: "Battlefish",
                newName: "BattleFishObject");

            migrationBuilder.AlterColumn<ulong>(
                name: "UserId",
                table: "BattleFishObject",
                type: "bigint unsigned",
                nullable: true,
                oldClrType: typeof(ulong));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BattleFishObject",
                table: "BattleFishObject",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleFishObject_UserId",
                table: "BattleFishObject",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleFishObject_Users_UserId",
                table: "BattleFishObject",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
