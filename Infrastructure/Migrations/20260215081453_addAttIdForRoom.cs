using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAttIdForRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttId",
                table: "Room",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 1,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 2,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 3,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 4,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 5,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 6,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 7,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 8,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 9,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 10,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 11,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 12,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 13,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 14,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 15,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 16,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 17,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 18,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 19,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 20,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 21,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 22,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 23,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 24,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 25,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 26,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 27,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 28,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 29,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 30,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 31,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 32,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 33,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 34,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 35,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 36,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 37,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 38,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 39,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 40,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 41,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 42,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 43,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 44,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 45,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 46,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 47,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 48,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 49,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 50,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 51,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 52,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 53,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 54,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 55,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 56,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 57,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 58,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 59,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 60,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 61,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 62,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 63,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 64,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 65,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 66,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 67,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 68,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 69,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 70,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 71,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 72,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 73,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 74,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 75,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 76,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 77,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 78,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 79,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 80,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 81,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 82,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 83,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 84,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 85,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 86,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 87,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 88,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 89,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 90,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 91,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 92,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 93,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 94,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 95,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 96,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 97,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 98,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 99,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 100,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 101,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 102,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 103,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 104,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 105,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 106,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 107,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 108,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 109,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 110,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 111,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 112,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 113,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 114,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 115,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 116,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 117,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 118,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 119,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 120,
                column: "AttId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttId",
                table: "Room");
        }
    }
}
