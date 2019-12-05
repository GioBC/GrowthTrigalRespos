using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class merdovulta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Measurers_MeasurersId",
                table: "Homes");

            migrationBuilder.RenameColumn(
                name: "MeasurersId",
                table: "Homes",
                newName: "MeasurerId");

            migrationBuilder.RenameIndex(
                name: "IX_Homes_MeasurersId",
                table: "Homes",
                newName: "IX_Homes_MeasurerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Measurers_MeasurerId",
                table: "Homes",
                column: "MeasurerId",
                principalTable: "Measurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Measurers_MeasurerId",
                table: "Homes");

            migrationBuilder.RenameColumn(
                name: "MeasurerId",
                table: "Homes",
                newName: "MeasurersId");

            migrationBuilder.RenameIndex(
                name: "IX_Homes_MeasurerId",
                table: "Homes",
                newName: "IX_Homes_MeasurersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Measurers_MeasurersId",
                table: "Homes",
                column: "MeasurersId",
                principalTable: "Measurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
