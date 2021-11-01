using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizationTables");

            migrationBuilder.DropTable(
                name: "ThemeTables");

            migrationBuilder.DropTable(
                name: "AppConfigTables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfigTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AppConfigId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SystemLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizationTables_AppConfigTables_AppConfigId",
                        column: x => x.AppConfigId,
                        principalTable: "AppConfigTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThemeTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AppConfigId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    FontSize = table.Column<int>(type: "int", nullable: false),
                    FrontFamily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainAccept = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondaryAccent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThemeTables_AppConfigTables_AppConfigId",
                        column: x => x.AppConfigId,
                        principalTable: "AppConfigTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationTables_AppConfigId",
                table: "LocalizationTables",
                column: "AppConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThemeTables_AppConfigId",
                table: "ThemeTables",
                column: "AppConfigId",
                unique: true);
        }
    }
}
