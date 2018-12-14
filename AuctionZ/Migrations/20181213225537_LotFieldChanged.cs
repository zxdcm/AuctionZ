using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionZ.Migrations
{
    public partial class LotFieldChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Lots",
                newName: "IsFinished");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Lots",
                newName: "IsActive");
        }
    }
}
