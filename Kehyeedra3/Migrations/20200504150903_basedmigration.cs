using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class basedmigration : Migration
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
                    RodOwned = table.Column<byte>(nullable: false),
                    RodUsed = table.Column<byte>(nullable: false),
                    Prestige = table.Column<int>(nullable: false),
                    Inventory = table.Column<string>(type: "LONGTEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<ulong>(nullable: false),
                    Send = table.Column<ulong>(nullable: false),
                    UserId = table.Column<ulong>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreFronts",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<ulong>(nullable: false),
                    StoreItemType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreFronts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Avatar = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Money = table.Column<long>(nullable: false),
                    LastMine = table.Column<ulong>(nullable: false),
                    GeneralInventory = table.Column<string>(type: "LONGTEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemOffer",
                columns: table => new
                {
                    OfferId = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyerId = table.Column<ulong>(nullable: false),
                    StoreId = table.Column<ulong>(nullable: false),
                    ItemId = table.Column<ulong>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    OfferAmount = table.Column<int>(nullable: false),
                    IsPurchaseFromStore = table.Column<bool>(nullable: false),
                    IsSellOffer = table.Column<bool>(nullable: false),
                    StoreFrontId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOffer", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_ItemOffer_StoreFronts_StoreFrontId",
                        column: x => x.StoreFrontId,
                        principalTable: "StoreFronts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreInventory",
                columns: table => new
                {
                    InvId = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Item = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    StoreFrontId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInventory", x => x.InvId);
                    table.ForeignKey(
                        name: "FK_StoreInventory_StoreFronts_StoreFrontId",
                        column: x => x.StoreFrontId,
                        principalTable: "StoreFronts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ItemOffer_StoreFrontId",
                table: "ItemOffer",
                column: "StoreFrontId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventory_StoreFrontId",
                table: "StoreInventory",
                column: "StoreFrontId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleFishObject");

            migrationBuilder.DropTable(
                name: "Fishing");

            migrationBuilder.DropTable(
                name: "ItemOffer");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "StoreInventory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StoreFronts");
        }
    }
}
