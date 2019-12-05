using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class ArregloDBHomeandFlowersSeedMeasurer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlowerId",
                table: "Measurers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurers_FlowerId",
                table: "Measurers",
                column: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurers_Flowers_FlowerId",
                table: "Measurers",
                column: "FlowerId",
                principalTable: "Flowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurers_Flowers_FlowerId",
                table: "Measurers");

            migrationBuilder.DropIndex(
                name: "IX_Measurers_FlowerId",
                table: "Measurers");

            migrationBuilder.DropColumn(
                name: "FlowerId",
                table: "Measurers");
        }
    }
}
