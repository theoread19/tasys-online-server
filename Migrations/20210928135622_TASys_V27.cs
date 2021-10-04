using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartTables_UserAccountTables_UserAccountId",
                table: "CartTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_UserAccountTables_InstructorId",
                table: "CourseTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriCulumTables_CourseTables_CourseId",
                table: "CurriCulumTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_RecipientId",
                table: "MessageTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikeTables_PostTables_PostId",
                table: "PostLikeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                table: "PostLikeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.DropForeignKey(
                name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                table: "StreamSessionTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRankingTables_CourseTables_CourseId",
                table: "UserRankingTables");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRankingTables_UserAccountTables_UserId",
                table: "UserRankingTables");

            migrationBuilder.AddColumn<int>(
                name: "EndTime",
                table: "ScheduleTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartTime",
                table: "ScheduleTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CartTables_UserAccountTables_UserAccountId",
                table: "CartTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_CurriCulumTables_CourseTables_CourseId",
                table: "CurriCulumTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_RecipientId",
                table: "MessageTables",
                column: "RecipientId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikeTables_PostTables_PostId",
                table: "PostLikeTables",
                column: "PostId",
                principalTable: "PostTables",
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

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRankingTables_CourseTables_CourseId",
                table: "UserRankingTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRankingTables_UserAccountTables_UserId",
                table: "UserRankingTables",
                column: "UserId",
                principalTable: "UserAccountTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartTables_UserAccountTables_UserAccountId",
                table: "CartTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTables_UserAccountTables_UserAccountId",
                table: "CommentTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTables_UserAccountTables_InstructorId",
                table: "CourseTables");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriCulumTables_CourseTables_CourseId",
                table: "CurriCulumTables");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTables_UserAccountTables_RecipientId",
                table: "MessageTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikeTables_PostTables_PostId",
                table: "PostLikeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                table: "PostLikeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTables_UserAccountTables_UserAccountId",
                table: "PostTables");

            migrationBuilder.DropForeignKey(
                name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                table: "StreamSessionTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRankingTables_CourseTables_CourseId",
                table: "UserRankingTables");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRankingTables_UserAccountTables_UserId",
                table: "UserRankingTables");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ScheduleTables");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ScheduleTables");

            migrationBuilder.AddForeignKey(
                name: "FK_CartTables_UserAccountTables_UserAccountId",
                table: "CartTables",
                column: "UserAccountId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurriCulumTables_CourseTables_CourseId",
                table: "CurriCulumTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTables_UserAccountTables_RecipientId",
                table: "MessageTables",
                column: "RecipientId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikeTables_PostTables_PostId",
                table: "PostLikeTables",
                column: "PostId",
                principalTable: "PostTables",
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

            migrationBuilder.AddForeignKey(
                name: "FK_TestResultTables_TestTables_TestId",
                table: "TestResultTables",
                column: "TestId",
                principalTable: "TestTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRankingTables_CourseTables_CourseId",
                table: "UserRankingTables",
                column: "CourseId",
                principalTable: "CourseTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRankingTables_UserAccountTables_UserId",
                table: "UserRankingTables",
                column: "UserId",
                principalTable: "UserAccountTables",
                principalColumn: "Id");
        }
    }
}
