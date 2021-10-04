using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTableScheduleTable");

            migrationBuilder.DropTable(
                name: "ScheduleTableUserAccountTable");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ScheduleTableUserAccountTable",
                columns: table => new
                {
                    SchedulesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserAccountsId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTableUserAccountTable", x => new { x.SchedulesId, x.UserAccountsId });
                    table.ForeignKey(
                        name: "FK_ScheduleTableUserAccountTable_ScheduleTables_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "ScheduleTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleTableUserAccountTable_UserAccountTables_UserAccountsId",
                        column: x => x.UserAccountsId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTableScheduleTable_SchedulesId",
                table: "CourseTableScheduleTable",
                column: "SchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTableUserAccountTable_UserAccountsId",
                table: "ScheduleTableUserAccountTable",
                column: "UserAccountsId");
        }
    }
}
