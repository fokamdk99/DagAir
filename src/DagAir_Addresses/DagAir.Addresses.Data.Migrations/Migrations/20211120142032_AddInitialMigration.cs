using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.Addresses.Data.Migrations.Migrations
{
    public partial class AddInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DagAir.Addresses");

            migrationBuilder.CreateTable(
                name: "cities",
                schema: "DagAir.Addresses",
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
                schema: "DagAir.Addresses",
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
                schema: "DagAir.Addresses",
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
                schema: "DagAir.Addresses",
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
                        principalSchema: "DagAir.Addresses",
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_addresses_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "DagAir.Addresses",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_addresses_postal_codes_postal_code_id",
                        column: x => x.postal_code_id,
                        principalSchema: "DagAir.Addresses",
                        principalTable: "postal_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Addresses",
                table: "cities",
                columns: new[] { "id", "created", "modified", "name" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Stockholm" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Reykjavik" }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Addresses",
                table: "countries",
                columns: new[] { "id", "created", "modified", "name" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sweden" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Iceland" }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Addresses",
                table: "postal_codes",
                columns: new[] { "id", "created", "modified", "number" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "04-265" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "25-685" }
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Addresses",
                table: "addresses",
                columns: new[] { "id", "city_id", "country_id", "created", "modified", "postal_code_id" },
                values: new object[] { 1L, 1L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1L });

            migrationBuilder.InsertData(
                schema: "DagAir.Addresses",
                table: "addresses",
                columns: new[] { "id", "city_id", "country_id", "created", "modified", "postal_code_id" },
                values: new object[] { 2L, 2L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2L });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_city_id",
                schema: "DagAir.Addresses",
                table: "addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_country_id",
                schema: "DagAir.Addresses",
                table: "addresses",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_postal_code_id",
                schema: "DagAir.Addresses",
                table: "addresses",
                column: "postal_code_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "DagAir.Addresses");

            migrationBuilder.DropTable(
                name: "cities",
                schema: "DagAir.Addresses");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "DagAir.Addresses");

            migrationBuilder.DropTable(
                name: "postal_codes",
                schema: "DagAir.Addresses");
        }
    }
}
