using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedTrainings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Training",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "StartDate", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 1", null, null },
                    { 2, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 2", null, null },
                    { 3, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 3", null, null },
                    { 4, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 4", null, null },
                    { 5, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 5", null, null },
                    { 6, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 6", null, null },
                    { 7, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 7", null, null },
                    { 8, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 8", null, null },
                    { 9, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 9", null, null },
                    { 10, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 10", null, null },
                    { 11, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 11", null, null },
                    { 12, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 12", null, null },
                    { 13, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 13", null, null },
                    { 14, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 14", null, null },
                    { 15, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 15", null, null },
                    { 16, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 16", null, null },
                    { 17, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 17", null, null },
                    { 18, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 18", null, null },
                    { 19, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 19", null, null },
                    { 20, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 20", null, null },
                    { 21, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 21", null, null },
                    { 22, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 22", null, null },
                    { 23, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 23", null, null },
                    { 24, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 24", null, null },
                    { 25, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 25", null, null },
                    { 26, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 26", null, null },
                    { 27, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 27", null, null },
                    { 28, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 28", null, null },
                    { 29, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 29", null, null },
                    { 30, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training 30", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
