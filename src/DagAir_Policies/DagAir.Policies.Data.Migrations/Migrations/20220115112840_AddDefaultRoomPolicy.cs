using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class AddDefaultRoomPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2023, 1, 15, 14, 28, 40, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 12, 28, 40, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2023, 1, 15, 13, 28, 40, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 9, 28, 40, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "created_by", "end_date", "end_hour", "expected_conditions_id", "modified", "repeat_on", "room_id", "spans_two_days", "start_date", "start_hour" },
                values: new object[] { 4L, 1L, "dd8db40b-c091-4d12-a185-b2f71f369917", new DateTime(9999, 12, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), 23, 2L, null, "", 0L, false, new DateTime(1980, 11, 5, 1, 1, 1, 0, DateTimeKind.Unspecified), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 4L);

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
        }
    }
}
