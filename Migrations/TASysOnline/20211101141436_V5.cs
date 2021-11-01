using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSuggestionTables");

            migrationBuilder.DropTable(
                name: "DiscountTables");

            migrationBuilder.DropTable(
                name: "TechnicalReportTables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseSuggestionTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LevelOfImportance = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Suggestion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSuggestionTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSuggestionTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscountTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalReportTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalReportTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalReportTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSuggestionTables_CourseId",
                table: "CourseSuggestionTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountTables_CourseId",
                table: "DiscountTables",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalReportTables_UserAccountId",
                table: "TechnicalReportTables",
                column: "UserAccountId");
        }
    }
}
