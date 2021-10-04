using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResultTableTestTable");

            migrationBuilder.AddColumn<string>(
                name: "TestId",
                table: "TestResultTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTables_TestId",
                table: "TestResultTables",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropIndex(
                name: "IX_TestResultTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestResultTables");

            migrationBuilder.CreateTable(
                name: "TestResultTableTestTable",
                columns: table => new
                {
                    TestResultsId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TestsId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultTableTestTable", x => new { x.TestResultsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_TestResultTableTestTable_TestResultTables_TestResultsId",
                        column: x => x.TestResultsId,
                        principalTable: "TestResultTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResultTableTestTable_TestTables_TestsId",
                        column: x => x.TestsId,
                        principalTable: "TestTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTableTestTable_TestsId",
                table: "TestResultTableTestTable",
                column: "TestsId");
        }
    }
}
