using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheAddress.DAL.Migrations
{
    public partial class Districtssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "tryy",
                table: "Properties",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tryy",
                table: "Properties");
        }
    }
}
