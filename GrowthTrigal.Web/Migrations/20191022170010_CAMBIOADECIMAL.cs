using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class CAMBIOADECIMAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Measure",
                table: "Measurements",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Measure",
                table: "Measurements",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldMaxLength: 5);
        }
    }
}
