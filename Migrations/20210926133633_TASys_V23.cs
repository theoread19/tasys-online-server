using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_PostTables_PostId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_PostTables_PostId",
                table: "CommentTables",
                column: "PostId",
                principalTable: "PostTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_PostTables_PostId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_PostTables_PostId",
                table: "CommentTables",
                column: "PostId",
                principalTable: "PostTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
