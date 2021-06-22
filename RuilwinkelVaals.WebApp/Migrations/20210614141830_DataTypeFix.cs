using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Migrations
{
    public partial class DataTypeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessDataId",
                table: "UserDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas",
                column: "BusinessDataId",
                principalTable: "BusinessDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessDataId",
                table: "UserDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas",
                column: "BusinessDataId",
                principalTable: "BusinessDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
