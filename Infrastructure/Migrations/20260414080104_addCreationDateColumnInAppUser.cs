using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCreationDateColumnInAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatingDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "CreatingDate",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id");
        }
    }
}
