using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAnswersTableWithBindingSurveyWithTraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_SurveyCategories_CategoryId",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Survey",
                newName: "TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_Survey_CategoryId",
                table: "Survey",
                newName: "IX_Survey_TrainingId");

            migrationBuilder.AddColumn<int>(
                name: "SurveyCategoryId",
                table: "Survey",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_SurveyCategoryId",
                table: "Survey",
                column: "SurveyCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_SurveyCategories_SurveyCategoryId",
                table: "Survey",
                column: "SurveyCategoryId",
                principalTable: "SurveyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_SurveyCategories_SurveyCategoryId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_Training_TrainingId",
                table: "Survey");

            migrationBuilder.DropIndex(
                name: "IX_Survey_SurveyCategoryId",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "SurveyCategoryId",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "TrainingId",
                table: "Survey",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Survey_TrainingId",
                table: "Survey",
                newName: "IX_Survey_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_SurveyCategories_CategoryId",
                table: "Survey",
                column: "CategoryId",
                principalTable: "SurveyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
