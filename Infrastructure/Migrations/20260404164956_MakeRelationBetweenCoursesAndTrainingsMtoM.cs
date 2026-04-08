using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeRelationBetweenCoursesAndTrainingsMtoM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Training_TrainingId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TrainingId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "CoursesTrainings",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesTrainings", x => new { x.CourseId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_CoursesTrainings_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesTrainings_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesTrainings_TrainingId",
                table: "CoursesTrainings",
                column: "TrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesTrainings");

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_TrainingId",
                table: "Course",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Training_TrainingId",
                table: "Course",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
