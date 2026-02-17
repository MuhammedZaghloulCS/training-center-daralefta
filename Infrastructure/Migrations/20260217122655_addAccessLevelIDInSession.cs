using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAccessLevelIDInSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccessLevelId",
                table: "Session",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 11,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 12,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 13,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 14,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 15,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 16,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 17,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 18,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 19,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 20,
                column: "AccessLevelId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessLevelId",
                table: "Session");
        }
    }
}
