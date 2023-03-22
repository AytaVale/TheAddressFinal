using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheAddress.DAL.Migrations
{
    public partial class Districtsssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tryy",
                table: "Properties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "tryy",
                table: "Properties",
                type: "bit",
                nullable: true);
        }
    }
}
