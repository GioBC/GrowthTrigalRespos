using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class newcolecctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Measurements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_HomeId",
                table: "Measurements",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Homes_HomeId",
                table: "Measurements",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Homes_HomeId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_HomeId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Measurements");
        }
    }
}
