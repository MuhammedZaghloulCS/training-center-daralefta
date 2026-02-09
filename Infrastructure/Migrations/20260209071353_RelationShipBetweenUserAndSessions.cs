using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationShipBetweenUserAndSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "lecturerId",
                table: "Session",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 1,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 2,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 3,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 4,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 5,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 6,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 7,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 8,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 9,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 10,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 11,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 12,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 13,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 14,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 15,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 16,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 17,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 18,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 19,
                column: "lecturerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Session",
                keyColumn: "Id",
                keyValue: 20,
                column: "lecturerId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Session_lecturerId",
                table: "Session",
                column: "lecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_AspNetUsers_lecturerId",
                table: "Session",
                column: "lecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_AspNetUsers_lecturerId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_lecturerId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "lecturerId",
                table: "Session");
        }
    }
}
