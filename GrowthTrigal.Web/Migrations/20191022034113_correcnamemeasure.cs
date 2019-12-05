using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class correcnamemeasure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mesaurement",
                table: "Measurements",
                newName: "Mesaure");

            migrationBuilder.RenameColumn(
                name: "MeasurementDate",
                table: "Measurements",
                newName: "MeasureDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mesaure",
                table: "Measurements",
                newName: "Mesaurement");

            migrationBuilder.RenameColumn(
                name: "MeasureDate",
                table: "Measurements",
                newName: "MeasurementDate");
        }
    }
}
