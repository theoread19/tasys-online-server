using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.AddColumn<string>(
                name: "UserAccountId",
                table: "TestResultTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTables_UserAccountId",
                table: "TestResultTables",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                table: "TestResultTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                table: "TestResultTables");

            migrationBuilder.DropIndex(
                name: "IX_TestResultTables_UserAccountId",
                table: "TestResultTables");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "TestResultTables");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
