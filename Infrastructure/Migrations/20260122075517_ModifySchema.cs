using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Room_SessionId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_AspNetUsers_CreatedByUserId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Survey_surveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_Survey_SurveyId",
                table: "SurveyResponse");

            migrationBuilder.DropTable(
                name: "ApplicationUserCourse");

            migrationBuilder.DropIndex(
                name: "IX_Session_SessionId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_TrainingId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "answer",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "surveyId",
                table: "SurveyQuestions",
                newName: "SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_surveyId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveyId");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "SurveyResponse",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Course",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyQuestionId = table.Column<int>(type: "int", nullable: false),
                    SurveyResponseId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyResponse_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_ApplicationUserId",
                table: "Course",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyQuestionId",
                table: "SurveyAnswers",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyResponseId_SurveyQuestionId",
                table: "SurveyAnswers",
                columns: new[] { "SurveyResponseId", "SurveyQuestionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_AspNetUsers_CreatedByUserId",
                table: "Survey",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_Survey_SurveyId",
                table: "SurveyResponse",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_AspNetUsers_CreatedByUserId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_Survey_SurveyId",
                table: "SurveyResponse");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Course_ApplicationUserId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "SurveyResponse");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "SurveyQuestions",
                newName: "surveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_surveyId");

            migrationBuilder.AddColumn<string>(
                name: "answer",
                table: "SurveyQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Session",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "Session",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserCourse",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    applicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCourse", x => new { x.CoursesId, x.applicationUsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_AspNetUsers_applicationUsersId",
                        column: x => x.applicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionId",
                table: "Session",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_TrainingId",
                table: "Session",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCourse_applicationUsersId",
                table: "ApplicationUserCourse",
                column: "applicationUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Room_SessionId",
                table: "Session",
                column: "SessionId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_AspNetUsers_CreatedByUserId",
                table: "Survey",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Survey_surveyId",
                table: "SurveyQuestions",
                column: "surveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_Survey_SurveyId",
                table: "SurveyResponse",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
