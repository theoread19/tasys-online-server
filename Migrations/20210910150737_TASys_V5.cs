using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountTables_CourseTables_CourseId",
                table: "DiscountTables");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonTables_CourseTables_CouresId",
                table: "LessonTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountTables_CourseTables_CourseId",
                table: "DiscountTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTables_CourseTables_CouresId",
                table: "LessonTables",
                column: "CouresId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountTables_CourseTables_CourseId",
                table: "DiscountTables");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonTables_CourseTables_CouresId",
                table: "LessonTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountTables_CourseTables_CourseId",
                table: "DiscountTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTables_CourseTables_CouresId",
                table: "LessonTables",
                column: "CouresId",
                principalTable: "CourseTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");
        }
    }
}
