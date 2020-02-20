using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class Fishing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fishing",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastFish = table.Column<ulong>(nullable: false),
                    Xp = table.Column<ulong>(nullable: false),
                    TXp = table.Column<ulong>(nullable: false),
                    Lvl = table.Column<ulong>(nullable: false),
                    CFish1 = table.Column<ulong>(nullable: false),
                    CFish2 = table.Column<ulong>(nullable: false),
                    CFish3 = table.Column<ulong>(nullable: false),
                    UFish1 = table.Column<ulong>(nullable: false),
                    UFish2 = table.Column<ulong>(nullable: false),
                    UFish3 = table.Column<ulong>(nullable: false),
                    RFish1 = table.Column<ulong>(nullable: false),
                    RFish2 = table.Column<ulong>(nullable: false),
                    RFish3 = table.Column<ulong>(nullable: false),
                    LFish = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishing", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fishing");
        }
    }
}
