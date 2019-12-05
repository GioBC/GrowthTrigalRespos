using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class ArregloDBHomeandFlowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Flowers_FlowerId",
                table: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_Homes_FlowerId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FlowerId",
                table: "Homes");

            migrationBuilder.AddColumn<int>(
                name: "HomesId",
                table: "Flowers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_HomesId",
                table: "Flowers",
                column: "HomesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Homes_HomesId",
                table: "Flowers",
                column: "HomesId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Homes_HomesId",
                table: "Flowers");

            migrationBuilder.DropIndex(
                name: "IX_Flowers_HomesId",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "HomesId",
                table: "Flowers");

            migrationBuilder.AddColumn<int>(
                name: "FlowerId",
                table: "Homes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_FlowerId",
                table: "Homes",
                column: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Flowers_FlowerId",
                table: "Homes",
                column: "FlowerId",
                principalTable: "Flowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
