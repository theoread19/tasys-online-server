using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                table: "TestResultTables");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                table: "TestResultTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                table: "TestResultTables");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                table: "TestResultTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
