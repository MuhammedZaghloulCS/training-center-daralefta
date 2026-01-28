using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedcourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "ApplicationUserId", "CreatedBy", "CreatedDate", "Description", "Duration", "Name", "Prerequisites", "TrainingId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 1", 11, "دورة رقم 1", "لا يوجد", null, null, null },
                    { 2, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 2", 12, "دورة رقم 2", "لا يوجد", null, null, null },
                    { 3, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 3", 13, "دورة رقم 3", "أساسيات الحاسوب", null, null, null },
                    { 4, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 4", 14, "دورة رقم 4", "لا يوجد", null, null, null },
                    { 5, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 5", 15, "دورة رقم 5", "لا يوجد", null, null, null },
                    { 6, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 6", 16, "دورة رقم 6", "أساسيات الحاسوب", null, null, null },
                    { 7, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 7", 17, "دورة رقم 7", "لا يوجد", null, null, null },
                    { 8, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 8", 18, "دورة رقم 8", "لا يوجد", null, null, null },
                    { 9, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 9", 19, "دورة رقم 9", "أساسيات الحاسوب", null, null, null },
                    { 10, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 10", 20, "دورة رقم 10", "لا يوجد", null, null, null },
                    { 11, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 11", 21, "دورة رقم 11", "لا يوجد", null, null, null },
                    { 12, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 12", 22, "دورة رقم 12", "أساسيات الحاسوب", null, null, null },
                    { 13, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 13", 23, "دورة رقم 13", "لا يوجد", null, null, null },
                    { 14, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 14", 24, "دورة رقم 14", "لا يوجد", null, null, null },
                    { 15, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 15", 25, "دورة رقم 15", "أساسيات الحاسوب", null, null, null },
                    { 16, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 16", 26, "دورة رقم 16", "لا يوجد", null, null, null },
                    { 17, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 17", 27, "دورة رقم 17", "لا يوجد", null, null, null },
                    { 18, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 18", 28, "دورة رقم 18", "أساسيات الحاسوب", null, null, null },
                    { 19, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 19", 29, "دورة رقم 19", "لا يوجد", null, null, null },
                    { 20, null, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 20", 10, "دورة رقم 20", "لا يوجد", null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
