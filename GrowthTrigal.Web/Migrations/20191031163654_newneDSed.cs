using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class newneDSed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurers_Homes_HomeId",
                table: "Measurers");

            migrationBuilder.DropIndex(
                name: "IX_Measurers_HomeId",
                table: "Measurers");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Measurers");

            migrationBuilder.AddColumn<int>(
                name: "MeasurersId",
                table: "Homes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_MeasurersId",
                table: "Homes",
                column: "MeasurersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Measurers_MeasurersId",
                table: "Homes",
                column: "MeasurersId",
                principalTable: "Measurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Measurers_MeasurersId",
                table: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_Homes_MeasurersId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "MeasurersId",
                table: "Homes");

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Measurers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurers_HomeId",
                table: "Measurers",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurers_Homes_HomeId",
                table: "Measurers",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
