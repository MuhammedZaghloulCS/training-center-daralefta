using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationsEnhancement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "lecturerId",
                table: "Session",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
