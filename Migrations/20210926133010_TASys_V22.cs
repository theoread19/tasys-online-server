using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "PostTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PostTables_CourseId",
                table: "PostTables",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_CourseTables_CourseId",
                table: "PostTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_CourseTables_CourseId",
                table: "PostTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.DropIndex(
                name: "IX_PostTables_CourseId",
                table: "PostTables");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "PostTables");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
