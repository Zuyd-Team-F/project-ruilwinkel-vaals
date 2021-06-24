using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Data.Migrations
{
    public partial class ChangeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles");
        }
    }
}
