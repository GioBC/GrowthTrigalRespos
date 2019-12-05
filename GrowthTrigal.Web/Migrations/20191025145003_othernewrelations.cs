using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class othernewrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MeasurerId",
                table: "Flowers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_MeasurerId",
                table: "Flowers",
                column: "MeasurerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Measurers_MeasurerId",
                table: "Flowers",
                column: "MeasurerId",
                principalTable: "Measurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Measurers_MeasurerId",
                table: "Flowers");

            migrationBuilder.DropIndex(
                name: "IX_Flowers_MeasurerId",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "MeasurerId",
                table: "Flowers");

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
    }
}
