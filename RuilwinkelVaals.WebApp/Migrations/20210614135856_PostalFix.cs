using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Migrations
{
    public partial class PostalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "UserDatas",
                type: "varchar(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 7)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLog",
                table: "ProductLogs",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostalCode",
                table: "UserDatas",
                type: "int",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldMaxLength: 7)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLog",
                table: "ProductLogs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
