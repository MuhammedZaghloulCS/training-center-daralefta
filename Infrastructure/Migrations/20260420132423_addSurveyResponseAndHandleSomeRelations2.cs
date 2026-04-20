using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSurveyResponseAndHandleSomeRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "SurveyResponse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_TrainingId",
                table: "SurveyResponse",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponse_Training_TrainingId",
                table: "SurveyResponse",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponse_Training_TrainingId",
                table: "SurveyResponse");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponse_TrainingId",
                table: "SurveyResponse");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "SurveyResponse");
        }
    }
}
