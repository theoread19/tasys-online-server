using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillTableCourseTable");

            migrationBuilder.DropTable(
                name: "BillTables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalItem = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillTableCourseTable",
                columns: table => new
                {
                    BillTablesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseTablesId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTableCourseTable", x => new { x.BillTablesId, x.CourseTablesId });
                    table.ForeignKey(
                        name: "FK_BillTableCourseTable_BillTables_BillTablesId",
                        column: x => x.BillTablesId,
                        principalTable: "BillTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillTableCourseTable_CourseTables_CourseTablesId",
                        column: x => x.CourseTablesId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillTableCourseTable_CourseTablesId",
                table: "BillTableCourseTable",
                column: "CourseTablesId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTables_UserAccountId",
                table: "BillTables",
                column: "UserAccountId");
        }
    }
}
