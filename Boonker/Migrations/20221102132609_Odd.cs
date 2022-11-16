using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boonker.Migrations
{
    public partial class Odd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DSCR = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DSCR = table.Column<string>(type: "text", nullable: true),
                    Img = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    CategoryID = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    IsFav = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_books_cats_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_CategoryID",
                table: "books",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "cats");
        }
    }
}
