using Microsoft.EntityFrameworkCore.Migrations;

namespace Boonker.Migrations
{
    public partial class lasthoup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sos",
                table: "Basket",
                newName: "Review");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Basket",
                newName: "AmountOf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Basket",
                newName: "Sos");

            migrationBuilder.RenameColumn(
                name: "AmountOf",
                table: "Basket",
                newName: "Amount");
        }
    }
}
