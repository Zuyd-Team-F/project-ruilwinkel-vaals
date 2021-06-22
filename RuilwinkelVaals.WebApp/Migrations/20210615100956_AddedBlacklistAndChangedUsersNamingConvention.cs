using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Migrations
{
    public partial class AddedBlacklistAndChangedUsersNamingConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanedProducts_UserDatas_UserId",
                table: "LoanedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_UserDatas_EmployeeId",
                table: "ProductLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDatas_Roles_RoleId",
                table: "UserDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDatas",
                table: "UserDatas");

            migrationBuilder.RenameTable(
                name: "UserDatas",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UserDatas_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDatas_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_UserDatas_BusinessDataId",
                table: "Users",
                newName: "IX_Users_BusinessDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Blacklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blacklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blacklist_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Blacklist_UserId",
                table: "Blacklist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanedProducts_Users_UserId",
                table: "LoanedProducts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_Users_EmployeeId",
                table: "ProductLogs",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BusinessDatas_BusinessDataId",
                table: "Users",
                column: "BusinessDataId",
                principalTable: "BusinessDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanedProducts_Users_UserId",
                table: "LoanedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_Users_EmployeeId",
                table: "ProductLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_BusinessDatas_BusinessDataId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Blacklist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserDatas");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "UserDatas",
                newName: "IX_UserDatas_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "UserDatas",
                newName: "IX_UserDatas_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_BusinessDataId",
                table: "UserDatas",
                newName: "IX_UserDatas_BusinessDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDatas",
                table: "UserDatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanedProducts_UserDatas_UserId",
                table: "LoanedProducts",
                column: "UserId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_UserDatas_EmployeeId",
                table: "ProductLogs",
                column: "EmployeeId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas",
                column: "BusinessDataId",
                principalTable: "BusinessDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatas_Roles_RoleId",
                table: "UserDatas",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
