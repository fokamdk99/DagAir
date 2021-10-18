﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.Sensors.Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
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
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
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
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
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
                    room_id = table.Column<long>(type: "bigint", nullable: false),
                    affiliate_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
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
                columns: new[] { "id", "address_id", "created", "date_of_establishment", "modified", "name" },
                values: new object[] { 1L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 16, 23, 41, 20, 724, DateTimeKind.Local).AddTicks(4435), null, "Saturn" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "producers",
                columns: new[] { "id", "address_id", "created", "date_of_establishment", "modified", "name" },
                values: new object[] { 2L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 11, 23, 41, 20, 727, DateTimeKind.Local).AddTicks(4005), null, "Euro agd" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensor_models",
                columns: new[] { "id", "created", "modified", "name", "producer_id", "version" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "illuminati", 1L, "v1" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensor_models",
                columns: new[] { "id", "created", "modified", "name", "producer_id", "version" },
                values: new object[] { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "humidati", 1L, "v1" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensor_models",
                columns: new[] { "id", "created", "modified", "name", "producer_id", "version" },
                values: new object[] { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "tempurati", 2L, "v1" });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensors",
                columns: new[] { "id", "affiliate_id", "created", "last_data_sent_date", "modified", "room_id", "sensor_model_id" },
                values: new object[] { 1L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 18, 18, 41, 20, 729, DateTimeKind.Local).AddTicks(6796), null, 1L, 1L });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensors",
                columns: new[] { "id", "affiliate_id", "created", "last_data_sent_date", "modified", "room_id", "sensor_model_id" },
                values: new object[] { 2L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 18, 20, 41, 20, 729, DateTimeKind.Local).AddTicks(7859), null, 1L, 2L });

            migrationBuilder.InsertData(
                schema: "DagAir.Sensors",
                table: "sensors",
                columns: new[] { "id", "affiliate_id", "created", "last_data_sent_date", "modified", "room_id", "sensor_model_id" },
                values: new object[] { 3L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 18, 22, 41, 20, 729, DateTimeKind.Local).AddTicks(7873), null, 1L, 3L });

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
