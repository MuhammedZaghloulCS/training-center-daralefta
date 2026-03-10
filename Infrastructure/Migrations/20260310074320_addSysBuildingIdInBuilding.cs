using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSysBuildingIdInBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SysBuildingId",
                table: "Building",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 1,
                column: "SysBuildingId",
                value: "8a807a299b0d347b019b0d355f9a0003");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 2,
                column: "SysBuildingId",
                value: "8a807a299b0d347b019b0d355f9a0003");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 3,
                column: "SysBuildingId",
                value: "8a807a299b0d347b019b0d355f9a0003");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 4,
                column: "SysBuildingId",
                value: "8a807a299b0d347b019b0d355f9a0003");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 5,
                column: "SysBuildingId",
                value: "8a807a299b0d347b019b0d355f9a0003");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SysBuildingId",
                table: "Building");
        }
    }
}
