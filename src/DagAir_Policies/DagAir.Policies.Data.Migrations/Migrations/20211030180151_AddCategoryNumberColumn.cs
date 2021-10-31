using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class AddCategoryNumberColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category_number",
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "room_policy_id",
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 10, 30, 22, 1, 51, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 20, 1, 51, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 10, 30, 21, 1, 51, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 17, 1, 51, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 2L,
                column: "category_number",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 3L,
                column: "category_number",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 4L,
                column: "category_number",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category_number",
                schema: "DagAir.Policies",
                table: "room_policy_categories");

            migrationBuilder.DropColumn(
                name: "room_policy_id",
                schema: "DagAir.Policies",
                table: "expected_room_conditions");

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 10, 25, 0, 48, 3, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 25, 22, 48, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "end_date", "start_date" },
                values: new object[] { new DateTime(2021, 10, 25, 23, 48, 3, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 25, 19, 48, 3, 0, DateTimeKind.Unspecified) });
        }
    }
}
