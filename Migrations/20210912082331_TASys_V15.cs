using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerTables_QuestionTables_QuestionId",
                table: "AnswerTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTables_TestTables_TestId",
                table: "QuestionTables");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerTables_QuestionTables_QuestionId",
                table: "AnswerTables",
                column: "QuestionId",
                principalTable: "QuestionTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables",
                column: "SenderId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTables_TestTables_TestId",
                table: "QuestionTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerTables_QuestionTables_QuestionId",
                table: "AnswerTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTables_TestTables_TestId",
                table: "QuestionTables");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerTables_QuestionTables_QuestionId",
                table: "AnswerTables",
                column: "QuestionId",
                principalTable: "QuestionTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_SenderId",
                table: "MessageTables",
                column: "SenderId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTables_TestTables_TestId",
                table: "QuestionTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id");
        }
    }
}
