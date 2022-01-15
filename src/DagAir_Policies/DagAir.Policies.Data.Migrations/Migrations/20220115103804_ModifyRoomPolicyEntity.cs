using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class ModifyRoomPolicyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "created_by",
                schema: "DagAir.Policies",
                table: "room_policies",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "end_hour",
                schema: "DagAir.Policies",
                table: "room_policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "start_hour",
                schema: "DagAir.Policies",
                table: "room_policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "temperature_margin",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "temperature",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "illuminance_margin",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "illuminance",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "humidity_margin",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "humidity",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "humidity", "humidity_margin", "illuminance", "illuminance_margin", "temperature", "temperature_margin" },
                values: new object[] { 0.4m, 0.1m, 100, 20m, 20m, 2m });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "humidity", "humidity_margin", "illuminance", "illuminance_margin", "temperature", "temperature_margin" },
                values: new object[] { 0.5m, 0.1m, 130, 30m, 22m, 3m });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_by", "end_date", "end_hour", "start_date", "start_hour" },
                values: new object[] { "dd8db40b-c091-4d12-a185-b2f71f369917", new DateTime(2023, 1, 15, 13, 38, 3, 0, DateTimeKind.Unspecified), 12, new DateTime(2022, 1, 15, 11, 38, 3, 0, DateTimeKind.Unspecified), 10 });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_by", "end_date", "end_hour", "start_date", "start_hour" },
                values: new object[] { "dd8db40b-c091-4d12-a185-b2f71f369917", new DateTime(2023, 1, 15, 12, 38, 3, 0, DateTimeKind.Unspecified), 14, new DateTime(2022, 1, 15, 8, 38, 3, 0, DateTimeKind.Unspecified), 12 });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "created_by", "end_date", "end_hour", "start_hour" },
                values: new object[] { "dd8db40b-c091-4d12-a185-b2f71f369917", new DateTime(2022, 10, 5, 23, 59, 59, 0, DateTimeKind.Unspecified), 6, 22 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "DagAir.Policies",
                table: "room_policies");

            migrationBuilder.DropColumn(
                name: "end_hour",
                schema: "DagAir.Policies",
                table: "room_policies");

            migrationBuilder.DropColumn(
                name: "start_hour",
                schema: "DagAir.Policies",
                table: "room_policies");

            migrationBuilder.AlterColumn<float>(
                name: "temperature_margin",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<float>(
                name: "temperature",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<float>(
                name: "illuminance_margin",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<float>(
                name: "illuminance",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "humidity_margin",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<float>(
                name: "humidity",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "humidity", "humidity_margin", "illuminance", "illuminance_margin", "temperature", "temperature_margin" },
                values: new object[] { 0.4f, 0.1f, 100f, 20f, 20f, 2f });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "humidity", "humidity_margin", "illuminance", "illuminance_margin", "temperature", "temperature_margin" },
                values: new object[] { 0.5f, 0.1f, 130f, 30f, 22f, 3f });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 11, 21, 1, 26, 22, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 21, 23, 26, 22, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 11, 21, 0, 26, 22, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 21, 20, 26, 22, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 3L,
                column: "end_date",
                value: new DateTime(2021, 10, 5, 23, 59, 59, 0, DateTimeKind.Unspecified));
        }
    }
}
