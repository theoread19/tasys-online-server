using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQuestions",
                table: "TestTables",
                newName: "MaxQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxQuestion",
                table: "TestTables",
                newName: "TotalQuestions");
        }
    }
}
