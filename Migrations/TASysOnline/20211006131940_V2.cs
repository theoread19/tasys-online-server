using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CourseTableScheduleTable",
                columns: table => new
                {
                    CoursesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SchedulesId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTableScheduleTable", x => new { x.CoursesId, x.SchedulesId });
                    table.ForeignKey(
                        name: "FK_CourseTableScheduleTable_CourseTables_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTableScheduleTable_ScheduleTables_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "ScheduleTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTableScheduleTable_SchedulesId",
                table: "CourseTableScheduleTable",
                column: "SchedulesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTableScheduleTable");

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
    }
}
