using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class noesclarecagu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UPs_Homes_HomeId",
                table: "UPs");

            migrationBuilder.DropIndex(
                name: "IX_UPs_HomeId",
                table: "UPs");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "UPs");

            migrationBuilder.AddColumn<int>(
                name: "UPsId",
                table: "Homes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_UPsId",
                table: "Homes",
                column: "UPsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_UPs_UPsId",
                table: "Homes",
                column: "UPsId",
                principalTable: "UPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_UPs_UPsId",
                table: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_Homes_UPsId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "UPsId",
                table: "Homes");

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "UPs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UPs_HomeId",
                table: "UPs",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UPs_Homes_HomeId",
                table: "UPs",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
