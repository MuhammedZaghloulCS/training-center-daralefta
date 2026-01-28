using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingBuilding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "يضم مكاتب الإدارة العليا والشؤون الإدارية والمالية", "المبنى الإداري الرئيسي", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 6, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "يضم مكاتب الإدارة العليا والشؤون الإدارية والمالية", "المبنى الإداري الرئيسي", null, null });
        }
    }
}
