using Microsoft.EntityFrameworkCore.Migrations;

namespace DagAir.Facilities.Data.Migrations.Migrations
{
    public partial class AddUniqueConstraintOnOrganizationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_organizations_name",
                schema: "DagAir.Facilities",
                table: "organizations",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_organizations_name",
                schema: "DagAir.Facilities",
                table: "organizations");
        }
    }
}
