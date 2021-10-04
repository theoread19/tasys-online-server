using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations
{
    public partial class TASys_V0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(1024)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AllocatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    TotalAttempt = table.Column<int>(type: "int", nullable: false),
                    MaxAttempt = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccountTables_RoleTables_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TotalCorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTables_TestTables_TestId",
                        column: x => x.TestId,
                        principalTable: "TestTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalItem = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TotalCourse = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Summary = table.Column<string>(type: "Text", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "Text", nullable: false),
                    AvailableSlot = table.Column<int>(type: "int", nullable: false),
                    MaxSlot = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Feedback = table.Column<string>(type: "Text", nullable: true),
                    RatingCount = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTables_SubjectTables_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SubjectTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseTables_UserAccountTables_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageTables_UserAccountTables_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageTables_UserAccountTables_SenderId",
                        column: x => x.SenderId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PostTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTableUserAccountTable",
                columns: table => new
                {
                    SchedulesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserAccountsId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTableUserAccountTable", x => new { x.SchedulesId, x.UserAccountsId });
                    table.ForeignKey(
                        name: "FK_ScheduleTableUserAccountTable_ScheduleTables_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "ScheduleTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleTableUserAccountTable_UserAccountTables_UserAccountsId",
                        column: x => x.UserAccountsId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamSessionTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPaticipants = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamSessionTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TechnicalReportTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "TestResultTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserAccountTableId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResultTables_UserAccountTables_UserAccountTableId",
                        column: x => x.UserAccountTableId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfoTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerTables_QuestionTables_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionTables",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "CartTableCourseTable",
                columns: table => new
                {
                    CartsId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CoursesId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartTableCourseTable", x => new { x.CartsId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CartTableCourseTable_CartTables_CartsId",
                        column: x => x.CartsId,
                        principalTable: "CartTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartTableCourseTable_CourseTables_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseSuggestionTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Suggestion = table.Column<string>(type: "text", nullable: false),
                    LevelOfImportance = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "CourseTableScheduleTable",
                columns: table => new
                {
                    CoursesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SchedulesId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTableScheduleTable", x => new { x.CoursesId, x.SchedulesId });
                    table.ForeignKey(
                        name: "FK_CourseTableScheduleTable_CourseTables_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTableScheduleTable_ScheduleTables_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "ScheduleTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTableUserAccountTable",
                columns: table => new
                {
                    CoursesOfLearnerId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LearnerAccountsId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTableUserAccountTable", x => new { x.CoursesOfLearnerId, x.LearnerAccountsId });
                    table.ForeignKey(
                        name: "FK_CourseTableUserAccountTable_CourseTables_CoursesOfLearnerId",
                        column: x => x.CoursesOfLearnerId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTableUserAccountTable_UserAccountTables_LearnerAccountsId",
                        column: x => x.LearnerAccountsId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurriCulumTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriCulumTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriCulumTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscountTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Duration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LessonTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BackText = table.Column<string>(type: "text", nullable: false),
                    FrontText = table.Column<string>(type: "text", nullable: false),
                    CouresId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTables_CourseTables_CouresId",
                        column: x => x.CouresId,
                        principalTable: "CourseTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRankingTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRankingTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRankingTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRankingTables_UserAccountTables_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentTables_PostTables_PostId",
                        column: x => x.PostId,
                        principalTable: "PostTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommentTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostLikeTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikeTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLikeTables_PostTables_PostId",
                        column: x => x.PostId,
                        principalTable: "PostTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StreamSessionTableUserAccountTable",
                columns: table => new
                {
                    ParticipantsId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StreamSessionsAttendedId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamSessionTableUserAccountTable", x => new { x.ParticipantsId, x.StreamSessionsAttendedId });
                    table.ForeignKey(
                        name: "FK_StreamSessionTableUserAccountTable_StreamSessionTables_StreamSessionsAttendedId",
                        column: x => x.StreamSessionsAttendedId,
                        principalTable: "StreamSessionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StreamSessionTableUserAccountTable_UserAccountTables_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResultTableTestTable",
                columns: table => new
                {
                    TestResultsId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TestsId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultTableTestTable", x => new { x.TestResultsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_TestResultTableTestTable_TestResultTables_TestResultsId",
                        column: x => x.TestResultsId,
                        principalTable: "TestResultTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResultTableTestTable_TestTables_TestsId",
                        column: x => x.TestsId,
                        principalTable: "TestTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Subtitle = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserInfoId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AnswerId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaTables_AnswerTables_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "AnswerTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaTables_FileTables_FileId",
                        column: x => x.FileId,
                        principalTable: "FileTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaTables_PostTables_PostId",
                        column: x => x.PostId,
                        principalTable: "PostTables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaTables_UserInfoTables_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfoTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTables_QuestionId",
                table: "AnswerTables",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTableCourseTable_CourseTablesId",
                table: "BillTableCourseTable",
                column: "CourseTablesId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTables_UserAccountId",
                table: "BillTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CartTableCourseTable_CoursesId",
                table: "CartTableCourseTable",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_CartTables_UserAccountId",
                table: "CartTables",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentTables_PostId",
                table: "CommentTables",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTables_UserAccountId",
                table: "CommentTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSuggestionTables_CourseId",
                table: "CourseSuggestionTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTables_InstructorId",
                table: "CourseTables",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTables_SubjectId",
                table: "CourseTables",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTableScheduleTable_SchedulesId",
                table: "CourseTableScheduleTable",
                column: "SchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTableUserAccountTable_LearnerAccountsId",
                table: "CourseTableUserAccountTable",
                column: "LearnerAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriCulumTables_CourseId",
                table: "CurriCulumTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountTables_CourseId",
                table: "DiscountTables",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonTables_CouresId",
                table: "LessonTables",
                column: "CouresId");

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

            migrationBuilder.CreateIndex(
                name: "IX_MessageTables_RecipientId",
                table: "MessageTables",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTables_SenderId",
                table: "MessageTables",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTables_UserAccountId",
                table: "NotificationTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikeTables_PostId",
                table: "PostLikeTables",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikeTables_UserAccountId",
                table: "PostLikeTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTables_TestId",
                table: "QuestionTables",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTableUserAccountTable_UserAccountsId",
                table: "ScheduleTableUserAccountTable",
                column: "UserAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessionTables_CreatorId",
                table: "StreamSessionTables",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessionTableUserAccountTable_StreamSessionsAttendedId",
                table: "StreamSessionTableUserAccountTable",
                column: "StreamSessionsAttendedId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalReportTables_UserAccountId",
                table: "TechnicalReportTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTables_UserAccountTableId",
                table: "TestResultTables",
                column: "UserAccountTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTableTestTable_TestsId",
                table: "TestResultTableTestTable",
                column: "TestsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountTables_RoleId",
                table: "UserAccountTables",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoTables_UserAccountId",
                table: "UserInfoTables",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRankingTables_CourseId",
                table: "UserRankingTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRankingTables_UserId",
                table: "UserRankingTables",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillTableCourseTable");

            migrationBuilder.DropTable(
                name: "CartTableCourseTable");

            migrationBuilder.DropTable(
                name: "CommentTables");

            migrationBuilder.DropTable(
                name: "CourseSuggestionTables");

            migrationBuilder.DropTable(
                name: "CourseTableScheduleTable");

            migrationBuilder.DropTable(
                name: "CourseTableUserAccountTable");

            migrationBuilder.DropTable(
                name: "CurriCulumTables");

            migrationBuilder.DropTable(
                name: "DiscountTables");

            migrationBuilder.DropTable(
                name: "LessonTables");

            migrationBuilder.DropTable(
                name: "MediaTables");

            migrationBuilder.DropTable(
                name: "MessageTables");

            migrationBuilder.DropTable(
                name: "NotificationTables");

            migrationBuilder.DropTable(
                name: "PostLikeTables");

            migrationBuilder.DropTable(
                name: "ScheduleTableUserAccountTable");

            migrationBuilder.DropTable(
                name: "StreamSessionTableUserAccountTable");

            migrationBuilder.DropTable(
                name: "TechnicalReportTables");

            migrationBuilder.DropTable(
                name: "TestResultTableTestTable");

            migrationBuilder.DropTable(
                name: "UserRankingTables");

            migrationBuilder.DropTable(
                name: "BillTables");

            migrationBuilder.DropTable(
                name: "CartTables");

            migrationBuilder.DropTable(
                name: "AnswerTables");

            migrationBuilder.DropTable(
                name: "FileTables");

            migrationBuilder.DropTable(
                name: "UserInfoTables");

            migrationBuilder.DropTable(
                name: "PostTables");

            migrationBuilder.DropTable(
                name: "ScheduleTables");

            migrationBuilder.DropTable(
                name: "StreamSessionTables");

            migrationBuilder.DropTable(
                name: "TestResultTables");

            migrationBuilder.DropTable(
                name: "CourseTables");

            migrationBuilder.DropTable(
                name: "QuestionTables");

            migrationBuilder.DropTable(
                name: "SubjectTables");

            migrationBuilder.DropTable(
                name: "UserAccountTables");

            migrationBuilder.DropTable(
                name: "TestTables");

            migrationBuilder.DropTable(
                name: "RoleTables");
        }
    }
}
