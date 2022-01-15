using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Facilities.Data.Migrations.Migrations
{
    public partial class RemoveUniqueRoomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unique_room_id",
                schema: "DagAir.Facilities",
                table: "rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "unique_room_id",
                schema: "DagAir.Facilities",
                table: "rooms",
                type: "varbinary(16)",
                nullable: false,
                defaultValueSql: "(UUID_TO_BIN(UUID()))");

            migrationBuilder.UpdateData(
                schema: "DagAir.Facilities",
                table: "rooms",
                keyColumn: "id",
                keyValue: 1L,
                column: "unique_room_id",
                value: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "DagAir.Facilities",
                table: "rooms",
                keyColumn: "id",
                keyValue: 2L,
                column: "unique_room_id",
                value: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "DagAir.Facilities",
                table: "rooms",
                keyColumn: "id",
                keyValue: 3L,
                column: "unique_room_id",
                value: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
        }
    }
}
