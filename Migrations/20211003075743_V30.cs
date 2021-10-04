using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class V30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTables_CourseTables_CourseId",
                table: "ScheduleTables");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTables_UserAccountTables_UserAccountId",
                table: "ScheduleTables");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleTables_CourseId",
                table: "ScheduleTables");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleTables_UserAccountId",
                table: "ScheduleTables");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ScheduleTables");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "ScheduleTables");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleId",
                table: "CourseTables",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTables_ScheduleId",
                table: "CourseTables",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTables_ScheduleTables_ScheduleId",
                table: "CourseTables",
                column: "ScheduleId",
                principalTable: "ScheduleTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_ScheduleTables_ScheduleId",
                table: "CourseTables");

            migrationBuilder.DropIndex(
                name: "IX_CourseTables_ScheduleId",
                table: "CourseTables");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "CourseTables");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "ScheduleTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAccountId",
                table: "ScheduleTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTables_CourseId",
                table: "ScheduleTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTables_UserAccountId",
                table: "ScheduleTables",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTables_CourseTables_CourseId",
                table: "ScheduleTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTables_UserAccountTables_UserAccountId",
                table: "ScheduleTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
