using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_room_policies_room_policy_configurations_category_id",
                schema: "DagAir.Policies",
                table: "room_policies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_room_policy_configurations",
                schema: "DagAir.Policies",
                table: "room_policy_configurations");

            migrationBuilder.RenameTable(
                name: "room_policy_configurations",
                schema: "DagAir.Policies",
                newName: "room_policy_categories",
                newSchema: "DagAir.Policies");

            migrationBuilder.AddPrimaryKey(
                name: "pk_room_policy_categories",
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                column: "id");

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                columns: new[] { "id", "created", "humidity", "humidity_margin", "illuminance", "illuminance_margin", "modified", "temperature", "temperature_margin" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.4f, 0.1f, 100f, 20f, null, 20f, 2f },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.5f, 0.1f, 130f, 30f, null, 22f, 3f }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                columns: new[] { "id", "created", "modified", "name" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Default" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Organizational" },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departmental" },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Custom" }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "created", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 1L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 25, 0, 48, 3, 0, DateTimeKind.Unspecified), 1L, null, "Monday, Thursday", 1L, new DateTime(2021, 10, 25, 22, 48, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "created", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 2L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 25, 23, 48, 3, 0, DateTimeKind.Unspecified), 2L, null, "Wednesday", 1L, new DateTime(2021, 10, 25, 19, 48, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "fk_room_policies_room_policy_categories_category_id",
                schema: "DagAir.Policies",
                table: "room_policies",
                column: "category_id",
                principalSchema: "DagAir.Policies",
                principalTable: "room_policy_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_room_policies_room_policy_categories_category_id",
                schema: "DagAir.Policies",
                table: "room_policies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_room_policy_categories",
                schema: "DagAir.Policies",
                table: "room_policy_categories");

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policies",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.RenameTable(
                name: "room_policy_categories",
                schema: "DagAir.Policies",
                newName: "room_policy_configurations",
                newSchema: "DagAir.Policies");

            migrationBuilder.AddPrimaryKey(
                name: "pk_room_policy_configurations",
                schema: "DagAir.Policies",
                table: "room_policy_configurations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_room_policies_room_policy_configurations_category_id",
                schema: "DagAir.Policies",
                table: "room_policies",
                column: "category_id",
                principalSchema: "DagAir.Policies",
                principalTable: "room_policy_configurations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
