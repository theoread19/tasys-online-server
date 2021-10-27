using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTableScheduleTable");

            migrationBuilder.DropTable(
                name: "ScheduleTables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTables", x => x.Id);
                });

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
    }
}
