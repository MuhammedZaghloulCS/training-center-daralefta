using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeRelationBetweenSessionAndTraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "Session",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_TrainingId",
                table: "Session",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Training_TrainingId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_TrainingId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Session");
        }
    }
}
