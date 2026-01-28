using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "BuildId", "Capacity", "CreatedBy", "CreatedDate", "HaveProjector", "Location", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, 30, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "الدور الأول - المبنى الإداري", "قاعة الاجتماعات الكبرى", null, null },
                    { 2, 1, 10, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "الدور الأرضي - المبنى الإداري", "مكتب شؤون الموظفين", null, null },
                    { 3, 2, 80, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "الدور الثاني - مبنى القاعات الدراسية", "قاعة محاضرات 1", null, null },
                    { 4, 2, 60, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "الدور الأول - مبنى القاعات الدراسية", "قاعة محاضرات 2", null, null },
                    { 5, 3, 25, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "الدور الأرضي - مبنى المعامل", "معمل حاسب آلي 1", null, null },
                    { 6, 3, 20, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "الدور الأول - مبنى المعامل", "معمل شبكات", null, null },
                    { 7, 4, 15, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "الدور الأرضي - مبنى شؤون الطلاب", "مكتب تسجيل الطلاب", null, null },
                    { 8, 5, 50, "system", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "الدور الأول - مبنى الخدمات", "قاعة أنشطة طلابية", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
