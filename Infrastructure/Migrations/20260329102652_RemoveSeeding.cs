using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 5);

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

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 10);

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

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 30);

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
                table: "Training",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "AccessLevelId",
                table: "Session");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessLevelId",
                table: "Session",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsDeleted", "Name", "SysBuildingId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 2, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مخصص للمحاضرات والدروس النظرية ويحتوي على قاعات مجهزة", false, "مبنى القاعات الدراسية", "8a807a299b0d347b019b0d355f9a0003", null, null },
                    { 3, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "يحتوي على معامل الحاسب الآلي والمعامل العملية", false, "مبنى المعامل والتطبيقات", "8a807a299b0d347b019b0d355f9a0003", null, null },
                    { 4, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مسؤول عن تسجيل الطلاب وتقديم الخدمات الطلابية", false, "مبنى شؤون الطلاب", "8a807a299b0d347b019b0d355f9a0003", null, null },
                    { 5, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "يضم الكافيتريا والخدمات العامة وقاعات الأنشطة", false, "مبنى الخدمات", "8a807a299b0d347b019b0d355f9a0003", null, null }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Duration", "IsDeleted", "Name", "Prerequisites", "TrainingId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 1", 11, false, "دورة رقم 1", "لا يوجد", null, null, null },
                    { 2, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 2", 12, false, "دورة رقم 2", "لا يوجد", null, null, null },
                    { 3, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 3", 13, false, "دورة رقم 3", "أساسيات الحاسوب", null, null, null },
                    { 4, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 4", 14, false, "دورة رقم 4", "لا يوجد", null, null, null },
                    { 5, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 5", 15, false, "دورة رقم 5", "لا يوجد", null, null, null },
                    { 6, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 6", 16, false, "دورة رقم 6", "أساسيات الحاسوب", null, null, null },
                    { 7, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 7", 17, false, "دورة رقم 7", "لا يوجد", null, null, null },
                    { 8, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 8", 18, false, "دورة رقم 8", "لا يوجد", null, null, null },
                    { 9, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 9", 19, false, "دورة رقم 9", "أساسيات الحاسوب", null, null, null },
                    { 10, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 10", 20, false, "دورة رقم 10", "لا يوجد", null, null, null },
                    { 11, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 11", 21, false, "دورة رقم 11", "لا يوجد", null, null, null },
                    { 12, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 12", 22, false, "دورة رقم 12", "أساسيات الحاسوب", null, null, null },
                    { 13, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 13", 23, false, "دورة رقم 13", "لا يوجد", null, null, null },
                    { 14, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 14", 24, false, "دورة رقم 14", "لا يوجد", null, null, null },
                    { 15, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 15", 25, false, "دورة رقم 15", "أساسيات الحاسوب", null, null, null },
                    { 16, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 16", 26, false, "دورة رقم 16", "لا يوجد", null, null, null },
                    { 17, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 17", 27, false, "دورة رقم 17", "لا يوجد", null, null, null },
                    { 18, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 18", 28, false, "دورة رقم 18", "أساسيات الحاسوب", null, null, null },
                    { 19, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 19", 29, false, "دورة رقم 19", "لا يوجد", null, null, null },
                    { 20, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "وصف مختصر للدورة رقم 20", 10, false, "دورة رقم 20", "لا يوجد", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Training",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "IsDeleted", "StartDate", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 1", null, null },
                    { 2, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 2", null, null },
                    { 3, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 3", null, null },
                    { 4, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 4", null, null },
                    { 5, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 5", null, null },
                    { 6, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 6", null, null },
                    { 7, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 7", null, null },
                    { 8, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 8", null, null },
                    { 9, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 9", null, null },
                    { 10, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 10", null, null },
                    { 11, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 11", null, null },
                    { 12, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 12", null, null },
                    { 13, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 13", null, null },
                    { 14, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 14", null, null },
                    { 15, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 15", null, null },
                    { 16, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 16", null, null },
                    { 17, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 17", null, null },
                    { 18, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 18", null, null },
                    { 19, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 19", null, null },
                    { 20, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 20", null, null },
                    { 21, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 21", null, null },
                    { 22, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 22", null, null },
                    { 23, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 23", null, null },
                    { 24, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 24", null, null },
                    { 25, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 25", null, null },
                    { 26, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 26", null, null },
                    { 27, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 27", null, null },
                    { 28, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 28", null, null },
                    { 29, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 29", null, null },
                    { 30, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 30", null, null }
                });

            migrationBuilder.InsertData(
                table: "Session",
                columns: new[] { "Id", "AccessLevelId", "CourseId", "CreatedBy", "CreatedDate", "EndTime", "IsDeleted", "RoomId", "SessionDate", "StartTime", "Topic", "UpdatedAt", "UpdatedBy", "lecturerId" },
                values: new object[,]
                {
                    { 1, "DEFAULT_ACCESS", 1, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), false, 20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "Session 1", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 2, "DEFAULT_ACCESS", 2, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), false, 21, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "Session 2", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 3, "DEFAULT_ACCESS", 3, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), false, 22, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "Session 3", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 4, "DEFAULT_ACCESS", 4, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), false, 23, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "Session 4", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 5, "DEFAULT_ACCESS", 5, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), false, 24, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "Session 5", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 6, "DEFAULT_ACCESS", 6, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), false, 25, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "Session 6", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 7, "DEFAULT_ACCESS", 7, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), false, 26, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "Session 7", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 8, "DEFAULT_ACCESS", 8, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), false, 27, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "Session 8", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 9, "DEFAULT_ACCESS", 9, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), false, 28, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "Session 9", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 10, "DEFAULT_ACCESS", 10, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), false, 29, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "Session 10", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") }
                });

            migrationBuilder.InsertData(
                table: "Survey",
                columns: new[] { "Id", "CreatedBy", "CreatedByUserId", "CreatedDate", "Description", "IsDeleted", "SurveyCategoryId", "Title", "TrainingId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 1", false, null, "Survey 1", 2, null, null },
                    { 2, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 2", false, null, "Survey 2", 3, null, null },
                    { 3, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 3", false, null, "Survey 3", 4, null, null },
                    { 4, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 4", false, null, "Survey 4", 5, null, null },
                    { 5, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 5", false, null, "Survey 5", 1, null, null },
                    { 6, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 6", false, null, "Survey 6", 2, null, null },
                    { 7, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 7", false, null, "Survey 7", 3, null, null },
                    { 8, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 8", false, null, "Survey 8", 4, null, null },
                    { 9, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 9", false, null, "Survey 9", 5, null, null },
                    { 10, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 10", false, null, "Survey 10", 1, null, null },
                    { 11, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 11", false, null, "Survey 11", 2, null, null },
                    { 12, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 12", false, null, "Survey 12", 3, null, null },
                    { 13, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 13", false, null, "Survey 13", 4, null, null },
                    { 14, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 14", false, null, "Survey 14", 5, null, null },
                    { 15, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 15", false, null, "Survey 15", 1, null, null },
                    { 16, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 16", false, null, "Survey 16", 2, null, null },
                    { 17, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 17", false, null, "Survey 17", 3, null, null },
                    { 18, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 18", false, null, "Survey 18", 4, null, null },
                    { 19, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 19", false, null, "Survey 19", 5, null, null },
                    { 20, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 20", false, null, "Survey 20", 1, null, null },
                    { 21, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 21", false, null, "Survey 21", 2, null, null },
                    { 22, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 22", false, null, "Survey 22", 3, null, null },
                    { 23, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 23", false, null, "Survey 23", 4, null, null },
                    { 24, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 24", false, null, "Survey 24", 5, null, null },
                    { 25, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 25", false, null, "Survey 25", 1, null, null },
                    { 26, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 26", false, null, "Survey 26", 2, null, null },
                    { 27, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 27", false, null, "Survey 27", 3, null, null },
                    { 28, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 28", false, null, "Survey 28", 4, null, null },
                    { 29, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 29", false, null, "Survey 29", 5, null, null },
                    { 30, "System", null, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description for Survey 30", false, null, "Survey 30", 1, null, null }
                });
        }
    }
}
