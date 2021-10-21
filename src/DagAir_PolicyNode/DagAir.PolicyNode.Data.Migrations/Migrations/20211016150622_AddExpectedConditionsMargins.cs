using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.PolicyNode.Data.Migrations.Migrations
{
    public partial class AddExpectedConditionsMargins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "humidity_margin",
                schema: "DagAir.PolicyNode",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "illuminance_margin",
                schema: "DagAir.PolicyNode",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "temperature_margin",
                schema: "DagAir.PolicyNode",
                table: "expected_room_conditions",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "humidity_margin",
                schema: "DagAir.PolicyNode",
                table: "expected_room_conditions");

            migrationBuilder.DropColumn(
                name: "illuminance_margin",
                schema: "DagAir.PolicyNode",
                table: "expected_room_conditions");

            migrationBuilder.DropColumn(
                name: "temperature_margin",
                schema: "DagAir.PolicyNode",
                table: "expected_room_conditions");
        }
    }
}
