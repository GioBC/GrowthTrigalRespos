using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class newrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
