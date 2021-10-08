using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.QueryNode.Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DagAir");

            migrationBuilder.CreateTable(
                name: "cities",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "postal_codes",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    number = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_postal_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    country_id = table.Column<long>(type: "bigint", nullable: false),
                    city_id = table.Column<long>(type: "bigint", nullable: false),
                    postal_code_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_cities_city_id",
                        column: x => x.city_id,
                        principalSchema: "DagAir",
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_addresses_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "DagAir",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_addresses_postal_codes_postal_code_id",
                        column: x => x.postal_code_id,
                        principalSchema: "DagAir",
                        principalTable: "postal_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    address_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizations_addresses_address_id",
                        column: x => x.address_id,
                        principalSchema: "DagAir",
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "producers",
                schema: "DagAir",
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
                    table.ForeignKey(
                        name: "fk_producers_addresses_address_id",
                        column: x => x.address_id,
                        principalSchema: "DagAir",
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "affiliates",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    organization_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_affiliates", x => x.id);
                    table.ForeignKey(
                        name: "fk_affiliates_organizations_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "DagAir",
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sensor_models",
                schema: "DagAir",
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
                        principalSchema: "DagAir",
                        principalTable: "producers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "DagAir",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    number = table.Column<string>(type: "text", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    affiliate_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_DATE)"),
                    modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_rooms_affiliates_affiliate_id",
                        column: x => x.affiliate_id,
                        principalSchema: "DagAir",
                        principalTable: "affiliates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sensors",
                schema: "DagAir",
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
                        name: "fk_sensors_affiliates_affiliate_id",
                        column: x => x.affiliate_id,
                        principalSchema: "DagAir",
                        principalTable: "affiliates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sensors_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "DagAir",
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sensors_sensor_models_sensor_model_id",
                        column: x => x.sensor_model_id,
                        principalSchema: "DagAir",
                        principalTable: "sensor_models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_city_id",
                schema: "DagAir",
                table: "addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_country_id",
                schema: "DagAir",
                table: "addresses",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_postal_code_id",
                schema: "DagAir",
                table: "addresses",
                column: "postal_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_affiliates_organization_id",
                schema: "DagAir",
                table: "affiliates",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_address_id",
                schema: "DagAir",
                table: "organizations",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_producers_address_id",
                schema: "DagAir",
                table: "producers",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rooms_affiliate_id",
                schema: "DagAir",
                table: "rooms",
                column: "affiliate_id");

            migrationBuilder.CreateIndex(
                name: "ix_sensor_models_producer_id",
                schema: "DagAir",
                table: "sensor_models",
                column: "producer_id");

            migrationBuilder.CreateIndex(
                name: "ix_sensors_affiliate_id",
                schema: "DagAir",
                table: "sensors",
                column: "affiliate_id");

            migrationBuilder.CreateIndex(
                name: "ix_sensors_room_id",
                schema: "DagAir",
                table: "sensors",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_sensors_sensor_model_id",
                schema: "DagAir",
                table: "sensors",
                column: "sensor_model_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sensors",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "sensor_models",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "affiliates",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "producers",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "organizations",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "addresses",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "cities",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "DagAir");

            migrationBuilder.DropTable(
                name: "postal_codes",
                schema: "DagAir");
        }
    }
}
