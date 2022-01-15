using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Sensors.Data.Migrations.Migrations
{
    public partial class AddSensorNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sensor_name",
                schema: "DagAir.Sensors",
                table: "sensors",
                type: "varchar(767)",
                nullable: true);

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
                columns: new[] { "last_data_sent_date", "sensor_name" },
                values: new object[] { new DateTime(2022, 1, 14, 10, 48, 57, 363, DateTimeKind.Local).AddTicks(1093), "com.gonzalo789.esp32" });

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "last_data_sent_date", "sensor_name" },
                values: new object[] { new DateTime(2022, 1, 14, 12, 48, 57, 363, DateTimeKind.Local).AddTicks(1946), "wemos_stas1" });

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "last_data_sent_date", "sensor_name" },
                values: new object[] { new DateTime(2022, 1, 14, 14, 48, 57, 363, DateTimeKind.Local).AddTicks(1962), "wemos_stas2" });

            migrationBuilder.CreateIndex(
                name: "ix_sensors_sensor_name",
                schema: "DagAir.Sensors",
                table: "sensors",
                column: "sensor_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_sensors_sensor_name",
                schema: "DagAir.Sensors",
                table: "sensors");

            migrationBuilder.DropColumn(
                name: "sensor_name",
                schema: "DagAir.Sensors",
                table: "sensors");

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "producers",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_of_establishment",
                value: new DateTime(2022, 1, 12, 15, 12, 53, 682, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "producers",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_of_establishment",
                value: new DateTime(2022, 1, 7, 15, 12, 53, 684, DateTimeKind.Local).AddTicks(4407));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 1L,
                column: "last_data_sent_date",
                value: new DateTime(2022, 1, 14, 10, 12, 53, 685, DateTimeKind.Local).AddTicks(9968));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 2L,
                column: "last_data_sent_date",
                value: new DateTime(2022, 1, 14, 12, 12, 53, 686, DateTimeKind.Local).AddTicks(912));

            migrationBuilder.UpdateData(
                schema: "DagAir.Sensors",
                table: "sensors",
                keyColumn: "id",
                keyValue: 3L,
                column: "last_data_sent_date",
                value: new DateTime(2022, 1, 14, 14, 12, 53, 686, DateTimeKind.Local).AddTicks(928));
        }
    }
}
