using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaTables_AnswerTables_AnswerId",
                table: "MediaTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTables_CourseTables_CourseId",
                table: "MediaTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTables_FileTables_FileId",
                table: "MediaTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTables_PostTables_PostId",
                table: "MediaTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTables_UserInfoTables_UserInfoId",
                table: "MediaTables");

            migrationBuilder.DropTable(
                name: "FileTables");

            migrationBuilder.DropIndex(
                name: "IX_MediaTables_AnswerId",
                table: "MediaTables");

            migrationBuilder.DropIndex(
                name: "IX_MediaTables_CourseId",
                table: "MediaTables");

            migrationBuilder.DropIndex(
                name: "IX_MediaTables_FileId",
                table: "MediaTables");

            migrationBuilder.DropIndex(
                name: "IX_MediaTables_PostId",
                table: "MediaTables");

            migrationBuilder.DropIndex(
                name: "IX_MediaTables_UserInfoId",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "MediaTables");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "MediaTables",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Container",
                table: "MediaTables",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FileSize",
                table: "MediaTables",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "MediaTables",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MediaTables",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceID",
                table: "MediaTables",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MediaTables",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "Container",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "SourceID",
                table: "MediaTables");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MediaTables");

            migrationBuilder.AddColumn<string>(
                name: "AnswerId",
                table: "MediaTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "MediaTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "MediaTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "MediaTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "MediaTables",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MediaTables",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserInfoId",
                table: "MediaTables",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(1024)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaTables_AnswerId",
                table: "MediaTables",
                column: "AnswerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaTables_CourseId",
                table: "MediaTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTables_FileId",
                table: "MediaTables",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaTables_PostId",
                table: "MediaTables",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTables_UserInfoId",
                table: "MediaTables",
                column: "UserInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTables_AnswerTables_AnswerId",
                table: "MediaTables",
                column: "AnswerId",
                principalTable: "AnswerTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTables_CourseTables_CourseId",
                table: "MediaTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTables_FileTables_FileId",
                table: "MediaTables",
                column: "FileId",
                principalTable: "FileTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTables_PostTables_PostId",
                table: "MediaTables",
                column: "PostId",
                principalTable: "PostTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTables_UserInfoTables_UserInfoId",
                table: "MediaTables",
                column: "UserInfoId",
                principalTable: "UserInfoTables",
                principalColumn: "Id");
        }
    }
}
