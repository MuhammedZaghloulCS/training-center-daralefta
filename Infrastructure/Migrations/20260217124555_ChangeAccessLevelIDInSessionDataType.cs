using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAccessLevelIDInSessionDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.AlterColumn<string>(
                name: "AccessLevelId",
                table: "Session",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 1,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 5,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 6,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 7,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 8,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 9,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 10,
                column: "AccessLevelId",
                value: "DEFAULT_ACCESS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AccessLevelId",
                table: "Session",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 1,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 5,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 6,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 7,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 8,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 9,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 10,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Session",
                columns: new[] { "Id", "AccessLevelId", "CourseId", "CreatedBy", "CreatedDate", "EndTime", "RoomId", "SessionDate", "StartTime", "Topic", "UpdatedAt", "UpdatedBy", "lecturerId" },
                values: new object[,]
                {
                    { 11, new Guid("00000000-0000-0000-0000-000000000000"), 11, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), 30, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Session 11", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 12, new Guid("00000000-0000-0000-0000-000000000000"), 12, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), 31, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Session 12", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 13, new Guid("00000000-0000-0000-0000-000000000000"), 13, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), 32, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Session 13", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 14, new Guid("00000000-0000-0000-0000-000000000000"), 14, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), 33, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Session 14", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 15, new Guid("00000000-0000-0000-0000-000000000000"), 15, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), 34, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Session 15", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 16, new Guid("00000000-0000-0000-0000-000000000000"), 16, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), 35, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Session 16", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 17, new Guid("00000000-0000-0000-0000-000000000000"), 17, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), 36, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Session 17", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 18, new Guid("00000000-0000-0000-0000-000000000000"), 18, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), 37, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Session 18", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 19, new Guid("00000000-0000-0000-0000-000000000000"), 19, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), 38, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Session 19", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") },
                    { 20, new Guid("00000000-0000-0000-0000-000000000000"), 20, "system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), 39, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Session 20", null, null, new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863") }
                });
        }
    }
}
