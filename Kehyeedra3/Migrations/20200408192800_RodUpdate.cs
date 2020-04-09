using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class RodUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RodOwned",
                table: "Fishing",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "RodUsed",
                table: "Fishing",
                nullable: false,
                defaultValue: (byte)0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ItemOffer_StoreFrontId",
                table: "ItemOffer",
                column: "StoreFrontId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOffer");

            migrationBuilder.DropColumn(
                name: "RodOwned",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "RodUsed",
                table: "Fishing");
        }
    }
}
