using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.Policies.Data.Migrations.Migrations
{
    public partial class AddInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DagAir.Policies");

            migrationBuilder.CreateTable(
                name: "expected_room_conditions",
                schema: "DagAir.Policies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    temperature = table.Column<float>(type: "float", nullable: false),
                    illuminance = table.Column<float>(type: "float", nullable: false),
                    humidity = table.Column<float>(type: "float", nullable: false),
                    temperature_margin = table.Column<float>(type: "float", nullable: false),
                    illuminance_margin = table.Column<float>(type: "float", nullable: false),
                    humidity_margin = table.Column<float>(type: "float", nullable: false),
                    room_policy_id = table.Column<long>(type: "bigint", nullable: true),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expected_room_conditions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_policy_categories",
                schema: "DagAir.Policies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    category_number = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_policy_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_policies",
                schema: "DagAir.Policies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    repeat_on = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    expected_conditions_id = table.Column<long>(type: "bigint", nullable: false),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    room_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_policies", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_policies_expected_room_conditions_expected_conditions_id",
                        column: x => x.expected_conditions_id,
                        principalSchema: "DagAir.Policies",
                        principalTable: "expected_room_conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_policies_room_policy_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "DagAir.Policies",
                        principalTable: "room_policy_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "expected_room_conditions",
                columns: new[] { "id", "humidity", "humidity_margin", "illuminance", "illuminance_margin", "modified", "room_policy_id", "temperature", "temperature_margin" },
                values: new object[,]
                {
                    { 1L, 0.4f, 0.1f, 100f, 20f, null, null, 20f, 2f },
                    { 2L, 0.5f, 0.1f, 130f, 30f, null, null, 22f, 3f }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policy_categories",
                columns: new[] { "id", "category_number", "modified", "name" },
                values: new object[,]
                {
                    { 1L, 0, null, "Default" },
                    { 2L, 1, null, "Organizational" },
                    { 3L, 2, null, "Departmental" },
                    { 4L, 3, null, "Custom" }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 1L, 1L, new DateTime(2021, 11, 21, 1, 26, 22, 0, DateTimeKind.Unspecified), 1L, null, "Monday, Thursday", 1L, new DateTime(2021, 11, 21, 23, 26, 22, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 2L, 2L, new DateTime(2021, 11, 21, 0, 26, 22, 0, DateTimeKind.Unspecified), 2L, null, "Wednesday", 1L, new DateTime(2021, 11, 21, 20, 26, 22, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "DagAir.Policies",
                table: "room_policies",
                columns: new[] { "id", "category_id", "end_date", "expected_conditions_id", "modified", "repeat_on", "room_id", "start_date" },
                values: new object[] { 3L, 2L, new DateTime(2021, 10, 5, 23, 59, 59, 0, DateTimeKind.Unspecified), 2L, null, "", 1L, new DateTime(2021, 11, 5, 1, 1, 1, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "ix_room_policies_category_id",
                schema: "DagAir.Policies",
                table: "room_policies",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_policies_expected_conditions_id",
                schema: "DagAir.Policies",
                table: "room_policies",
                column: "expected_conditions_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "room_policies",
                schema: "DagAir.Policies");

            migrationBuilder.DropTable(
                name: "expected_room_conditions",
                schema: "DagAir.Policies");

            migrationBuilder.DropTable(
                name: "room_policy_categories",
                schema: "DagAir.Policies");
        }
    }
}
