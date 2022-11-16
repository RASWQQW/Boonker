using Microsoft.EntityFrameworkCore.Migrations;

namespace Boonker.Migrations
{
    public partial class Odds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Books");
        }
    }
}
