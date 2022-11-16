using Microsoft.EntityFrameworkCore.Migrations;

namespace Boonker.Migrations
{
    public partial class LastOccured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatViews",
                table: "Cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatViews",
                table: "Cats");
        }
    }
}
