using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_SubjectTables_SubjectId",
                table: "CourseTables");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "CourseTables",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTables_SubjectTables_SubjectId",
                table: "CourseTables",
                column: "SubjectId",
                principalTable: "SubjectTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_SubjectTables_SubjectId",
                table: "CourseTables");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "CourseTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTables_SubjectTables_SubjectId",
                table: "CourseTables",
                column: "SubjectId",
                principalTable: "SubjectTables",
                principalColumn: "Id");
        }
    }
}
