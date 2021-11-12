using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAttempt",
                table: "TestTables");

            migrationBuilder.DropColumn(
                name: "MaxParticipants",
                table: "StreamSessionTables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAttempt",
                table: "TestTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxParticipants",
                table: "StreamSessionTables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
