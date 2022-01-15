using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.Sensors.Data.Migrations.Migrations
{
    public partial class AddInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DagAir.Sensors");

            migrationBuilder.CreateTable(
                name: "producers",
                schema: "DagAir.Sensors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    date_of_establishment = table.Column<DateTime>(type: "datetime", nullable: false),
                    address_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sensor_models",
                schema: "DagAir.Sensors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    version = table.Column<string>(type: "text", nullable: false),
                    producer_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sensor_models", x => x.id);
                    table.ForeignKey(
                        name: "fk_sensor_models_producers_producer_id",
                        column: x => x.producer_id,
                        principalSchema: "DagAir.Sensors",
                        principalTable: "producers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sensors",
                schema: "DagAir.Sensors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    last_data_sent_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    sensor_model_id = table.Column<long>(type: "bigint", nullable: false),
                    unique_room_id = table.Column<byte[]>(type: "varbinary(16)", nullable: false, defaultValueSql: "(UUID_TO_BIN(UUID()))"),
                    affiliate_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sensors", x => x.id);
                    table.ForeignKey(
                        name: "fk_sensors_sensor_models_sensor_model_id",
                        column: x => x.sensor_model_id,
                        principalSchema: "DagAir.Sensors",
                        principalTable: "sensor_models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "producers",
                columns: new[] { "id", "address_id", "date_of_establishment", "modified", "name" },
                values: new object[] { 1L, 1L, new DateTime(2022, 1, 12, 15, 12, 53, 682, DateTimeKind.Local).AddTicks(1530), null, "Saturn" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "producers",
                columns: new[] { "id", "address_id", "date_of_establishment", "modified", "name" },
                values: new object[] { 2L, 2L, new DateTime(2022, 1, 7, 15, 12, 53, 684, DateTimeKind.Local).AddTicks(4407), null, "Euro agd" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensor_models",
                columns: new[] { "id", "modified", "name", "producer_id", "version" },
                values: new object[] { 1L, null, "illuminati", 1L, "v1" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensor_models",
                columns: new[] { "id", "modified", "name", "producer_id", "version" },
                values: new object[] { 2L, null, "humidati", 1L, "v1" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensor_models",
                columns: new[] { "id", "modified", "name", "producer_id", "version" },
                values: new object[] { 3L, null, "tempurati", 2L, "v1" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensors",
                columns: new[] { "id", "affiliate_id", "last_data_sent_date", "modified", "sensor_model_id", },
                values: new object[] { 1L, 1L, new DateTime(2022, 1, 14, 10, 12, 53, 685, DateTimeKind.Local).AddTicks(9968), null, 1L });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensors",
                columns: new[] { "id", "affiliate_id", "last_data_sent_date", "modified", "sensor_model_id" },
                values: new object[] { 2L, 1L, new DateTime(2022, 1, 14, 12, 12, 53, 686, DateTimeKind.Local).AddTicks(912), null, 2L});

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensors",
                columns: new[] { "id", "affiliate_id", "last_data_sent_date", "modified", "sensor_model_id" },
                values: new object[] { 3L, 1L, new DateTime(2022, 1, 14, 14, 12, 53, 686, DateTimeKind.Local).AddTicks(928), null, 3L });

            migrationBuilder.CreateIndex(
                name: "ix_sensor_models_producer_id",
                schema: "DagAir.Sensors",
                table: "sensor_models",
                column: "producer_id");

            migrationBuilder.CreateIndex(
                name: "ix_sensors_sensor_model_id",
                schema: "DagAir.Sensors",
                table: "sensors",
                column: "sensor_model_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sensors",
                schema: "DagAir.Sensors");

            migrationBuilder.DropTable(
                name: "sensor_models",
                schema: "DagAir.Sensors");

            migrationBuilder.DropTable(
                name: "producers",
                schema: "DagAir.Sensors");
        }
    }
}
