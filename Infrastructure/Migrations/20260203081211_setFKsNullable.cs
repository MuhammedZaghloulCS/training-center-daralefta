using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class setFKsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_SurveyCategories_SurveyCategoryId",
                table: "Survey");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyCategoryId",
                table: "Survey",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedByUserId",
                table: "Survey",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Survey",
                columns: new[] { "Id", "CreatedBy", "CreatedByUserId", "CreatedDate", "Description", "SurveyCategoryId", "Title", "TrainingId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 1", null, "Survey 1", 2, null, null },
                    { 2, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 2", null, "Survey 2", 3, null, null },
                    { 3, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 3", null, "Survey 3", 4, null, null },
                    { 4, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 4", null, "Survey 4", 5, null, null },
                    { 5, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 5", null, "Survey 5", 1, null, null },
                    { 6, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 6", null, "Survey 6", 2, null, null },
                    { 7, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 7", null, "Survey 7", 3, null, null },
                    { 8, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 8", null, "Survey 8", 4, null, null },
                    { 9, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 9", null, "Survey 9", 5, null, null },
                    { 10, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 10", null, "Survey 10", 1, null, null },
                    { 11, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 11", null, "Survey 11", 2, null, null },
                    { 12, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 12", null, "Survey 12", 3, null, null },
                    { 13, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 13", null, "Survey 13", 4, null, null },
                    { 14, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 14", null, "Survey 14", 5, null, null },
                    { 15, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 15", null, "Survey 15", 1, null, null },
                    { 16, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 16", null, "Survey 16", 2, null, null },
                    { 17, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 17", null, "Survey 17", 3, null, null },
                    { 18, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 18", null, "Survey 18", 4, null, null },
                    { 19, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 19", null, "Survey 19", 5, null, null },
                    { 20, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 20", null, "Survey 20", 1, null, null },
                    { 21, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 21", null, "Survey 21", 2, null, null },
                    { 22, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 22", null, "Survey 22", 3, null, null },
                    { 23, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 23", null, "Survey 23", 4, null, null },
                    { 24, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 24", null, "Survey 24", 5, null, null },
                    { 25, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 25", null, "Survey 25", 1, null, null },
                    { 26, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 26", null, "Survey 26", 2, null, null },
                    { 27, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 27", null, "Survey 27", 3, null, null },
                    { 28, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 28", null, "Survey 28", 4, null, null },
                    { 29, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 29", null, "Survey 29", 5, null, null },
                    { 30, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 30", null, "Survey 30", 1, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_SurveyCategories_SurveyCategoryId",
                table: "Survey",
                column: "SurveyCategoryId",
                principalTable: "SurveyCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_SurveyCategories_SurveyCategoryId",
                table: "Survey");

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.AlterColumn<int>(
                name: "SurveyCategoryId",
                table: "Survey",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedByUserId",
                table: "Survey",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_SurveyCategories_SurveyCategoryId",
                table: "Survey",
                column: "SurveyCategoryId",
                principalTable: "SurveyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
