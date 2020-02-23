using Microsoft.EntityFrameworkCore.Migrations;

namespace Kehyeedra3.Migrations
{
    public partial class feesh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CFish1",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "CFish2",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "CFish3",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "LFish",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "RFish1",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "RFish2",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "RFish3",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "UFish1",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "UFish2",
                table: "Fishing");

            migrationBuilder.DropColumn(
                name: "UFish3",
                table: "Fishing");

            migrationBuilder.AddColumn<string>(
                name: "Inventory",
                table: "Fishing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Fishing");

            migrationBuilder.AddColumn<ulong>(
                name: "CFish1",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "CFish2",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "CFish3",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "LFish",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "RFish1",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "RFish2",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "RFish3",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "UFish1",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "UFish2",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "UFish3",
                table: "Fishing",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);
        }
    }
}
