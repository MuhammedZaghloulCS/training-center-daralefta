using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSurveyResponseAndHandleSomeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse");

            migrationBuilder.DropIndex(
                name: "IX_SurveyAnswers_SurveyResponseId_SurveyQuestionId",
                table: "SurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Survey_TrainingId",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Survey");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyQuestionId",
                table: "SurveyAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "questionId",
                table: "SurveyAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainingsSurveys",
                columns: table => new
                {
                    trainingId = table.Column<int>(type: "int", nullable: false),
                    surveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingsSurveys", x => new { x.trainingId, x.surveyId });
                    table.ForeignKey(
                        name: "FK_TrainingsSurveys_Survey_surveyId",
                        column: x => x.surveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingsSurveys_Training_trainingId",
                        column: x => x.trainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_questionId",
                table: "SurveyAnswers",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyResponseId_questionId",
                table: "SurveyAnswers",
                columns: new[] { "SurveyResponseId", "questionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsSurveys_surveyId",
                table: "TrainingsSurveys",
                column: "surveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswers_Question_questionId",
                table: "SurveyAnswers",
                column: "questionId",
                principalTable: "Question",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswers_Question_questionId",
                table: "SurveyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse");

            migrationBuilder.DropTable(
                name: "TrainingsSurveys");

            migrationBuilder.DropIndex(
                name: "IX_SurveyAnswers_questionId",
                table: "SurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_SurveyAnswers_SurveyResponseId_questionId",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "questionId",
                table: "SurveyAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyQuestionId",
                table: "SurveyAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "Survey",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyResponseId_SurveyQuestionId",
                table: "SurveyAnswers",
                columns: new[] { "SurveyResponseId", "SurveyQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_TrainingId",
                table: "Survey",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_AspNetUsers_UserId",
                table: "SurveyResponse",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
