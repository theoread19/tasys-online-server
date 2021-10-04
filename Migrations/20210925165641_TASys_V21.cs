using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreamSessionTableUserAccountTable");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "StreamSessionTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessionTables_CourseId",
                table: "StreamSessionTables",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSessionTables_CourseTables_CourseId",
                table: "StreamSessionTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamSessionTables_CourseTables_CourseId",
                table: "StreamSessionTables");

            migrationBuilder.DropIndex(
                name: "IX_StreamSessionTables_CourseId",
                table: "StreamSessionTables");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "StreamSessionTables");

            migrationBuilder.CreateTable(
                name: "StreamSessionTableUserAccountTable",
                columns: table => new
                {
                    ParticipantsId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StreamSessionsAttendedId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamSessionTableUserAccountTable", x => new { x.ParticipantsId, x.StreamSessionsAttendedId });
                    table.ForeignKey(
                        name: "FK_StreamSessionTableUserAccountTable_StreamSessionTables_StreamSessionsAttendedId",
                        column: x => x.StreamSessionsAttendedId,
                        principalTable: "StreamSessionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StreamSessionTableUserAccountTable_UserAccountTables_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessionTableUserAccountTable_StreamSessionsAttendedId",
                table: "StreamSessionTableUserAccountTable",
                column: "StreamSessionsAttendedId");
        }
    }
}
