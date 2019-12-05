using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class ArregloDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedName",
                table: "Homes");

            migrationBuilder.AddColumn<int>(
                name: "FlowerId",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BedName",
                table: "Flowers",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BedName",
                table: "Flowers");

            migrationBuilder.AddColumn<string>(
                name: "BedName",
                table: "Homes",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }
    }
}
