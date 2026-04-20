using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fullChangeSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_AspNetUsers_CreatedByUserId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Survey_CreatedByUserId",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Survey",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Survey",
                newName: "FireDate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Survey",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Survey",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "Survey",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Survey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Survey",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveyId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Survey_SurveyId1",
                        column: x => x.SurveyId1,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_SurveyId1",
                table: "Question",
                column: "SurveyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Survey",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Survey",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "FireDate",
                table: "Survey",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Survey",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "Survey",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Survey",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Survey",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Survey",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Survey",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_CreatedByUserId",
                table: "Survey",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_AspNetUsers_CreatedByUserId",
                table: "Survey",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id");
        }
    }
}
