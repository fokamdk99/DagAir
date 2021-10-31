using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class AddNewRoomPolicySeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 10, 30, 22, 24, 6, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 20, 24, 6, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "created", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 3L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 5, 23, 59, 59, 0, DateTimeKind.Unspecified), 2L, null, "", 1L, new DateTime(2021, 11, 5, 1, 1, 1, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 10, 30, 22, 1, 51, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 20, 1, 51, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "created", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 2L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 21, 1, 51, 0, DateTimeKind.Unspecified), 2L, null, "Wednesday", 1L, new DateTime(2021, 10, 30, 17, 1, 51, 0, DateTimeKind.Unspecified) });
        }
    }
}
