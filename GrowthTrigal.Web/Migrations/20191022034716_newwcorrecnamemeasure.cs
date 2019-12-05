using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class newwcorrecnamemeasure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mesaure",
                table: "Measurements",
                newName: "Measure");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Measure",
                table: "Measurements",
                newName: "Mesaure");
        }
    }
}
