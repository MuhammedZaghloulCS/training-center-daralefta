using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEntranceAndExitDoorssIdsInRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttRoomId",
                table: "Room",
                newName: "AttRoomIdinside");

            migrationBuilder.AddColumn<string>(
                name: "AttRoomIdOutSide",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 1,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 2,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 3,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 4,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 5,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 6,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 7,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 8,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 9,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 10,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 11,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 12,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 13,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 14,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 15,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 16,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 17,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 18,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 19,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 20,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 21,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 22,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 23,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 24,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 25,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 26,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 27,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 28,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 29,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 30,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 31,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 32,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 33,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 34,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 35,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 36,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 37,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 38,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 39,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 40,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 41,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 42,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 43,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 44,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 45,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 46,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 47,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 48,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 49,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 50,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 51,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 52,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 53,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 54,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 55,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 56,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 57,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 58,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 59,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 60,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 61,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 62,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 63,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 64,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 65,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 66,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 67,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 68,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 69,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 70,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 71,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 72,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 73,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 74,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 75,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 76,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 77,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 78,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 79,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 80,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 81,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 82,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 83,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 84,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 85,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 86,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 87,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 88,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 89,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 90,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 91,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 92,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 93,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 94,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 95,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 96,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 97,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 98,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 99,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 100,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 101,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 102,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 103,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 104,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 105,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 106,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 107,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 108,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 109,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 110,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 111,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 112,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 113,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 114,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 115,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 116,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 117,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 118,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 119,
                column: "AttRoomIdOutSide",
                value: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 120,
                column: "AttRoomIdOutSide",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttRoomIdOutSide",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "AttRoomIdinside",
                table: "Room",
                newName: "AttRoomId");
        }
    }
}
