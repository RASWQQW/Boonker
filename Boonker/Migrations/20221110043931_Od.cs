using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boonker.Migrations
{
    public partial class Od : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_Cats",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Cats",
                table: "Books",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Cats",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "tirage",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DSCR = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Books_Author_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cats_CategoryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "tirage",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Books",
                newName: "Cats");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                newName: "IX_Books_Cats");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cats_Cats",
                table: "Books",
                column: "Cats",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
