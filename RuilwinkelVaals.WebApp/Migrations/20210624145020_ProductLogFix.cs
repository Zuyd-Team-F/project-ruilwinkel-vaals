using Microsoft.EntityFrameworkCore.Migrations;

namespace RuilwinkelVaals.WebApp.Migrations
{
    public partial class ProductLogFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_ProductLogs_LogId",
                table: "ProductLogs");

            migrationBuilder.RenameColumn(
                name: "LogId",
                table: "ProductLogs",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductLogs_LogId",
                table: "ProductLogs",
                newName: "IX_ProductLogs_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_Product_ProductId",
                table: "ProductLogs",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_Product_ProductId",
                table: "ProductLogs");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductLogs",
                newName: "LogId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductLogs_ProductId",
                table: "ProductLogs",
                newName: "IX_ProductLogs_LogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_ProductLogs_LogId",
                table: "ProductLogs",
                column: "LogId",
                principalTable: "ProductLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
