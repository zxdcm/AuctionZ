using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionZ.Migrations
{
    public partial class RoleMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: 0);
        }
    }
}
