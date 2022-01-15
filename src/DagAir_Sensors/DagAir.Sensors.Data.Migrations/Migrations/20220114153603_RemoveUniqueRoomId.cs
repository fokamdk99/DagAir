using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Sensors.Data.Migrations.Migrations
{
    public partial class RemoveUniqueRoomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unique_room_id",
                schema: "DagAir.Sensors",
                table: "sensors");

            migrationBuilder.AddColumn<long>(
                name: "room_id",
                schema: "DagAir.Sensors",
                table: "sensors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "producers",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_of_establishment",
                value: new DateTime(2022, 1, 12, 16, 36, 3, 299, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "producers",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_of_establishment",
                value: new DateTime(2022, 1, 7, 16, 36, 3, 302, DateTimeKind.Local).AddTicks(9479));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "last_data_sent_date", "room_id" },
                values: new object[] { new DateTime(2022, 1, 14, 11, 36, 3, 304, DateTimeKind.Local).AddTicks(6060), 1L });

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "last_data_sent_date", "room_id" },
                values: new object[] { new DateTime(2022, 1, 14, 13, 36, 3, 304, DateTimeKind.Local).AddTicks(6737), 1L });

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "last_data_sent_date", "room_id" },
                values: new object[] { new DateTime(2022, 1, 14, 15, 36, 3, 304, DateTimeKind.Local).AddTicks(6746), 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "room_id",
                schema: "DagAir.Sensors",
                table: "sensors");

            migrationBuilder.AddColumn<byte[]>(
                name: "unique_room_id",
                schema: "DagAir.Sensors",
                table: "sensors",
                type: "varbinary(16)",
                nullable: false,
                defaultValueSql: "(UUID_TO_BIN(UUID()))");

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "producers",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_of_establishment",
                value: new DateTime(2022, 1, 12, 15, 48, 57, 359, DateTimeKind.Local).AddTicks(1954));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "producers",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_of_establishment",
                value: new DateTime(2022, 1, 7, 15, 48, 57, 361, DateTimeKind.Local).AddTicks(5538));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "last_data_sent_date", "unique_room_id" },
                values: new object[] { new DateTime(2022, 1, 14, 10, 48, 57, 363, DateTimeKind.Local).AddTicks(1093), new byte[] { 123, 181, 38, 17, 113, 169, 74, 71, 149, 203, 157, 14, 144, 106, 84, 194 } });

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "last_data_sent_date", "unique_room_id" },
                values: new object[] { new DateTime(2022, 1, 14, 12, 48, 57, 363, DateTimeKind.Local).AddTicks(1946), new byte[] { 88, 203, 13, 237, 89, 98, 72, 71, 138, 236, 46, 30, 106, 154, 238, 216 } });

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "last_data_sent_date", "unique_room_id" },
                values: new object[] { new DateTime(2022, 1, 14, 14, 48, 57, 363, DateTimeKind.Local).AddTicks(1962), new byte[] { 76, 238, 114, 188, 217, 61, 209, 67, 144, 60, 197, 210, 24, 56, 148, 182 } });
        }
    }
}
