using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DagAir.Facilities.Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DagAir.Facilities");

            migrationBuilder.CreateTable(
                name: "organizations",
                schema: "DagAir.Facilities",
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
                });

            migrationBuilder.CreateTable(
                name: "affiliates",
                schema: "DagAir.Facilities",
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
                        principalSchema: "DagAir.Facilities",
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "DagAir.Facilities",
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
                        principalSchema: "DagAir.Facilities",
                        principalTable: "affiliates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "organizations",
                columns: new[] { "id", "address_id", "created", "modified", "name" },
                values: new object[] { 1L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Warsaw University Of Technology" });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "organizations",
                columns: new[] { "id", "address_id", "created", "modified", "name" },
                values: new object[] { 2L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Warsaw School Of Economics" });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "affiliates",
                columns: new[] { "id", "created", "modified", "name", "organization_id" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Faculty of Electronics and Information Technology", 1L });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "affiliates",
                columns: new[] { "id", "created", "modified", "name", "organization_id" },
                values: new object[] { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Faculty of Mathematics and Information Science", 1L });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "affiliates",
                columns: new[] { "id", "created", "modified", "name", "organization_id" },
                values: new object[] { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Collegium Of Economic Analysis", 2L });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "rooms",
                columns: new[] { "id", "affiliate_id", "created", "floor", "modified", "number" },
                values: new object[] { 1L, 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "133" });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "rooms",
                columns: new[] { "id", "affiliate_id", "created", "floor", "modified", "number" },
                values: new object[] { 2L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "117" });

            migrationBuilder.InsertData(
                schema: "DagAir.Facilities",
                table: "rooms",
                columns: new[] { "id", "affiliate_id", "created", "floor", "modified", "number" },
                values: new object[] { 3L, 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, "52" });

            migrationBuilder.CreateIndex(
                name: "ix_affiliates_organization_id",
                schema: "DagAir.Facilities",
                table: "affiliates",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_rooms_affiliate_id",
                schema: "DagAir.Facilities",
                table: "rooms",
                column: "affiliate_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rooms",
                schema: "DagAir.Facilities");

            migrationBuilder.DropTable(
                name: "affiliates",
                schema: "DagAir.Facilities");

            migrationBuilder.DropTable(
                name: "organizations",
                schema: "DagAir.Facilities");
        }
    }
}
