using Microsoft.EntityFrameworkCore.Migrations;

namespace Boonker.Migrations
{
    public partial class sos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Cats",
                table: "Books",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Cats",
                table: "Books",
                column: "Cats");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cats_Cats",
                table: "Books",
                column: "Cats",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_Cats",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Cats",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Cats",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
