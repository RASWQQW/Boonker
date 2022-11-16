using Microsoft.EntityFrameworkCore.Migrations;

namespace Boonker.Migrations
{
    public partial class Initer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_cats_CategoryID",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cats",
                table: "cats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.RenameTable(
                name: "cats",
                newName: "Cats");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Books",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_books_CategoryID",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Books",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cats",
                table: "Cats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cats",
                table: "Cats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Cats",
                newName: "cats");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "books",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "books",
                newName: "IX_books_CategoryID");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cats",
                table: "cats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_books_cats_CategoryID",
                table: "books",
                column: "CategoryID",
                principalTable: "cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
