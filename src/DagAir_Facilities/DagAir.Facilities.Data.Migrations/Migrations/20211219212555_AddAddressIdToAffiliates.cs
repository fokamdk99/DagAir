using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Facilities.Data.Migrations.Migrations
{
    public partial class AddAddressIdToAffiliates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "address_id",
                schema: "DagAir.Facilities",
                table: "affiliates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "DagAir.Facilities",
                table: "affiliates",
                keyColumn: "id",
                keyValue: 1L,
                column: "address_id",
                value: 1L);

            migrationBuilder.UpdateData(
                schema: "DagAir.Facilities",
                table: "affiliates",
                keyColumn: "id",
                keyValue: 2L,
                column: "address_id",
                value: 2L);

            migrationBuilder.UpdateData(
                schema: "DagAir.Facilities",
                table: "affiliates",
                keyColumn: "id",
                keyValue: 3L,
                column: "address_id",
                value: 2L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_id",
                schema: "DagAir.Facilities",
                table: "affiliates");
        }
    }
}
