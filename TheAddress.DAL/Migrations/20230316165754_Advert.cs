using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheAddress.DAL.Migrations
{
    public partial class Advert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Buy",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rent",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buy",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Rent",
                table: "Properties");
        }
    }
}
