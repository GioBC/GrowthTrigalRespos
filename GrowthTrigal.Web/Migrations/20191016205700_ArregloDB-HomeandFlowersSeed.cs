using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class ArregloDBHomeandFlowersSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Homes_HomesId",
                table: "Flowers");

            migrationBuilder.RenameColumn(
                name: "HomesId",
                table: "Flowers",
                newName: "HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Flowers_HomesId",
                table: "Flowers",
                newName: "IX_Flowers_HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Homes_HomeId",
                table: "Flowers",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Homes_HomeId",
                table: "Flowers");

            migrationBuilder.RenameColumn(
                name: "HomeId",
                table: "Flowers",
                newName: "HomesId");

            migrationBuilder.RenameIndex(
                name: "IX_Flowers_HomeId",
                table: "Flowers",
                newName: "IX_Flowers_HomesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Homes_HomesId",
                table: "Flowers",
                column: "HomesId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
