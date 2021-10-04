using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TASysOnlineProject.Migrations.TASysOnline
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SourceID = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Container = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileSize = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTables", x => x.Id);
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
                    StartTime = table.Column<float>(type: "real", nullable: false),
                    EndTime = table.Column<float>(type: "real", nullable: false),
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
                name: "UserAccountTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    InstructorId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ScheduleId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTables_ScheduleTables_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ScheduleTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseTables_SubjectTables_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SubjectTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTables_UserAccountTables_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageTables_UserAccountTables_SenderId",
                        column: x => x.SenderId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "UserInfoTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StreamSessionTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamSessionTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamSessionTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StreamSessionTables_UserAccountTables_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AllocatedTime = table.Column<int>(type: "int", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    TotalAttempt = table.Column<int>(type: "int", nullable: false),
                    MaxAttempt = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTables_CourseTables_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRankingTables_UserAccountTables_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikeTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResultTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    TestId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserAccountTableId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResultTables_TestTables_TestId",
                        column: x => x.TestId,
                        principalTable: "TestTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResultTables_UserAccountTables_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResultTables_UserAccountTables_UserAccountTableId",
                        column: x => x.UserAccountTableId,
                        principalTable: "UserAccountTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CourseTables_ScheduleId",
                table: "CourseTables",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTables_SubjectId",
                table: "CourseTables",
                column: "SubjectId");

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
                name: "IX_LessonTables_CourseId",
                table: "LessonTables",
                column: "CourseId");

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
                name: "IX_PostTables_CourseId",
                table: "PostTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTables_UserAccountId",
                table: "PostTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTables_TestId",
                table: "QuestionTables",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessionTables_CourseId",
                table: "StreamSessionTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamSessionTables_CreatorId",
                table: "StreamSessionTables",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalReportTables_UserAccountId",
                table: "TechnicalReportTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTables_TestId",
                table: "TestResultTables",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTables_UserAccountId",
                table: "TestResultTables",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultTables_UserAccountTableId",
                table: "TestResultTables",
                column: "UserAccountTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTables_CourseId",
                table: "TestTables",
                column: "CourseId");

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
                name: "AnswerTables");

            migrationBuilder.DropTable(
                name: "BillTableCourseTable");

            migrationBuilder.DropTable(
                name: "CartTableCourseTable");

            migrationBuilder.DropTable(
                name: "CommentTables");

            migrationBuilder.DropTable(
                name: "CourseSuggestionTables");

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
                name: "StreamSessionTables");

            migrationBuilder.DropTable(
                name: "TechnicalReportTables");

            migrationBuilder.DropTable(
                name: "TestResultTables");

            migrationBuilder.DropTable(
                name: "UserInfoTables");

            migrationBuilder.DropTable(
                name: "UserRankingTables");

            migrationBuilder.DropTable(
                name: "QuestionTables");

            migrationBuilder.DropTable(
                name: "BillTables");

            migrationBuilder.DropTable(
                name: "CartTables");

            migrationBuilder.DropTable(
                name: "PostTables");

            migrationBuilder.DropTable(
                name: "TestTables");

            migrationBuilder.DropTable(
                name: "CourseTables");

            migrationBuilder.DropTable(
                name: "ScheduleTables");

            migrationBuilder.DropTable(
                name: "SubjectTables");

            migrationBuilder.DropTable(
                name: "UserAccountTables");

            migrationBuilder.DropTable(
                name: "RoleTables");
        }
    }
}
