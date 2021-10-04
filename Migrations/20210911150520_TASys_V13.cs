using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonTables_CourseTables_CouresId",
                table: "LessonTables");

            migrationBuilder.DropIndex(
                name: "IX_LessonTables_CouresId",
                table: "LessonTables");

            migrationBuilder.DropColumn(
                name: "CouresId",
                table: "LessonTables");

            migrationBuilder.AddColumn<string>(
                name: "CourseTablesId",
                table: "LessonTables",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonTables_CourseTablesId",
                table: "LessonTables",
                column: "CourseTablesId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTables_CourseTables_CourseTablesId",
                table: "LessonTables",
                column: "CourseTablesId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonTables_CourseTables_CourseTablesId",
                table: "LessonTables");

            migrationBuilder.DropIndex(
                name: "IX_LessonTables_CourseTablesId",
                table: "LessonTables");

            migrationBuilder.DropColumn(
                name: "CourseTablesId",
                table: "LessonTables");

            migrationBuilder.AddColumn<string>(
                name: "CouresId",
                table: "LessonTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTables_CouresId",
                table: "LessonTables",
                column: "CouresId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTables_CourseTables_CouresId",
                table: "LessonTables",
                column: "CouresId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
