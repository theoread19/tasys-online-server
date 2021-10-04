using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CourseId",
                table: "LessonTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTables_CourseId",
                table: "LessonTables",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTables_CourseTables_CourseId",
                table: "LessonTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonTables_CourseTables_CourseId",
                table: "LessonTables");

            migrationBuilder.DropIndex(
                name: "IX_LessonTables_CourseId",
                table: "LessonTables");

            migrationBuilder.DropColumn(
                name: "CourseId",
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
    }
}
