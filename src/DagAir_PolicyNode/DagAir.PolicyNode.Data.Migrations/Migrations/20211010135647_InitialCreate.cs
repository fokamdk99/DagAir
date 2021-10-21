using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.PolicyNode.Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DagAir.PolicyNode");

            migrationBuilder.CreateTable(
                name: "expected_room_conditions",
                schema: "DagAir.PolicyNode",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    temperature = table.Column<float>(type: "float", nullable: false),
                    illuminance = table.Column<float>(type: "float", nullable: false),
                    humidity = table.Column<float>(type: "float", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expected_room_conditions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_policy_configurations",
                schema: "DagAir.PolicyNode",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_policy_configurations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_policies",
                schema: "DagAir.PolicyNode",
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
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_policies", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_policies_expected_room_conditions_expected_conditions_id",
                        column: x => x.expected_conditions_id,
                        principalSchema: "DagAir.PolicyNode",
                        principalTable: "expected_room_conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_policies_room_policy_configurations_category_id",
                        column: x => x.category_id,
                        principalSchema: "DagAir.PolicyNode",
                        principalTable: "room_policy_configurations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_room_policies_category_id",
                schema: "DagAir.PolicyNode",
                table: "room_policies",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_policies_expected_conditions_id",
                schema: "DagAir.PolicyNode",
                table: "room_policies",
                column: "expected_conditions_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "room_policies",
                schema: "DagAir.PolicyNode");

            migrationBuilder.DropTable(
                name: "expected_room_conditions",
                schema: "DagAir.PolicyNode");

            migrationBuilder.DropTable(
                name: "room_policy_configurations",
                schema: "DagAir.PolicyNode");
        }
    }
}
