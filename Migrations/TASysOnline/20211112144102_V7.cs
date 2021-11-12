using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_RecipientId",
                table: "MessageTables");

            migrationBuilder.DropTable(
                name: "NotificationTables");

            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "MessageTables");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "MessageTables",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageTables_RecipientId",
                table: "MessageTables",
                newName: "IX_MessageTables_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_CourseTables_CourseId",
                table: "MessageTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_CourseTables_CourseId",
                table: "MessageTables");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "MessageTables",
                newName: "RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageTables_CourseId",
                table: "MessageTables",
                newName: "IX_MessageTables_RecipientId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "MessageTables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "NotificationTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTables_UserAccountId",
                table: "NotificationTables",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_RecipientId",
                table: "MessageTables",
                column: "RecipientId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
