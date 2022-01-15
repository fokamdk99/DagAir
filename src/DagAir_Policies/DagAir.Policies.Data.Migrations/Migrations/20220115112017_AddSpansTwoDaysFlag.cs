using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class AddSpansTwoDaysFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "spans_two_days",
                schema: "DagAir.Policies",
                table: "room_policies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2023, 1, 15, 14, 20, 16, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 12, 20, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2023, 1, 15, 13, 20, 16, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 9, 20, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 3L,
                column: "spans_two_days",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "spans_two_days",
                schema: "DagAir.Policies",
                table: "room_policies");

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2023, 1, 15, 13, 38, 3, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 11, 38, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2023, 1, 15, 12, 38, 3, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 8, 38, 3, 0, DateTimeKind.Unspecified) });
        }
    }
}
