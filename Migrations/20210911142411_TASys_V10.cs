using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "TestTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TestTables_CourseId",
                table: "TestTables",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestTables_CourseTables_CourseId",
                table: "TestTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestTables_CourseTables_CourseId",
                table: "TestTables");

            migrationBuilder.DropIndex(
                name: "IX_TestTables_CourseId",
                table: "TestTables");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "TestTables");
        }
    }
}
