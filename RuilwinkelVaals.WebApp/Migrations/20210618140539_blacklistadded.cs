using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Migrations
{
    public partial class blacklistadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Blacklist",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blacklist",
                table: "Users");
        }
    }
}
