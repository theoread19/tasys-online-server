using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class V13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTables_UserAccountTables_UserAccountId",
                table: "BillTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_UserAccountTables_InstructorId",
                table: "CourseTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                table: "PostLikeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.DropForeignKey(
                name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                table: "StreamSessionTables");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTables_UserAccountTables_UserAccountId",
                table: "BillTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTables_UserAccountTables_InstructorId",
                table: "CourseTables",
                column: "InstructorId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables",
                column: "SenderId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                table: "PostLikeTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                table: "StreamSessionTables",
                column: "CreatorId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTables_UserAccountTables_UserAccountId",
                table: "BillTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_UserAccountTables_InstructorId",
                table: "CourseTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                table: "PostLikeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.DropForeignKey(
                name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                table: "StreamSessionTables");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTables_UserAccountTables_UserAccountId",
                table: "BillTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTables_UserAccountTables_InstructorId",
                table: "CourseTables",
                column: "InstructorId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables",
                column: "SenderId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                table: "PostLikeTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                table: "StreamSessionTables",
                column: "CreatorId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
