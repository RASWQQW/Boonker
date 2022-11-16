using Microsoft.EntityFrameworkCore.Migrations;

namespace Boonker.Migrations
{
    public partial class iis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Books",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
