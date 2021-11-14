using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "CourseTables");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CourseTables");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "CourseTables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "CourseTables",
                type: "Text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "CourseTables",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "CourseTables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
