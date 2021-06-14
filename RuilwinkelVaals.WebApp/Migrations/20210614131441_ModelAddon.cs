using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Migrations
{
    public partial class ModelAddon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "UserDatas",
                newName: "BusinessDataId");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "UserDatas",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserDatas",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserDatas",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserDatas",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Remarks",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_BusinessDataId",
                table: "UserDatas",
                column: "BusinessDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_RoleId",
                table: "UserDatas",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_ProductId",
                table: "Remarks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLogs_EmployeeId",
                table: "ProductLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLogs_LogId",
                table: "ProductLogs",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ConditionId",
                table: "Product",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Conditions_ConditionId",
                table: "Product",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_ProductLogs_LogId",
                table: "ProductLogs",
                column: "LogId",
                principalTable: "ProductLogs",
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
                name: "FK_Remarks_Product_ProductId",
                table: "Remarks",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas",
                column: "BusinessDataId",
                principalTable: "BusinessDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatas_Roles_RoleId",
                table: "UserDatas",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Conditions_ConditionId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_ProductLogs_LogId",
                table: "ProductLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_UserDatas_EmployeeId",
                table: "ProductLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_Product_ProductId",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDatas_BusinessDatas_BusinessDataId",
                table: "UserDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDatas_Roles_RoleId",
                table: "UserDatas");

            migrationBuilder.DropIndex(
                name: "IX_UserDatas_BusinessDataId",
                table: "UserDatas");

            migrationBuilder.DropIndex(
                name: "IX_UserDatas_RoleId",
                table: "UserDatas");

            migrationBuilder.DropIndex(
                name: "IX_Remarks_ProductId",
                table: "Remarks");

            migrationBuilder.DropIndex(
                name: "IX_ProductLogs_EmployeeId",
                table: "ProductLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductLogs_LogId",
                table: "ProductLogs");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ConditionId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "BusinessDataId",
                table: "UserDatas",
                newName: "BusinessId");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "UserDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserDatas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Remarks",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
