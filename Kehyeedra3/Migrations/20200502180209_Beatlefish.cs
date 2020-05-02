using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class Beatlefish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BattleFishObject",
                columns: table => new
                {
                    FishId = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FishType = table.Column<byte>(nullable: false),
                    Xp = table.Column<ulong>(nullable: false),
                    NextXp = table.Column<ulong>(nullable: false),
                    Lvl = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleFishObject", x => x.FishId);
                    table.ForeignKey(
                        name: "FK_BattleFishObject_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleFishObject_UserId",
                table: "BattleFishObject",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleFishObject");
        }
    }
}
