using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowthTrigal.Web.Migrations
{
    public partial class CompleteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockNumber = table.Column<int>(maxLength: 2, nullable: false),
                    BedName = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UPs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FarmName = table.Column<int>(maxLength: 25, nullable: false),
                    AliasFarm = table.Column<string>(maxLength: 4, nullable: false),
                    HomeId = table.Column<int>(nullable: true),
                    FlowerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UPs_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UPs_Homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mesaurement = table.Column<float>(maxLength: 5, nullable: false),
                    MeasurementDate = table.Column<DateTime>(nullable: false),
                    FlowerId = table.Column<int>(nullable: true),
                    UPId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Measurements_UPs_UPId",
                        column: x => x.UPId,
                        principalTable: "UPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_FlowerId",
                table: "Measurements",
                column: "FlowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_UPId",
                table: "Measurements",
                column: "UPId");

            migrationBuilder.CreateIndex(
                name: "IX_UPs_FlowerId",
                table: "UPs",
                column: "FlowerId");

            migrationBuilder.CreateIndex(
                name: "IX_UPs_HomeId",
                table: "UPs",
                column: "HomeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "UPs");

            migrationBuilder.DropTable(
                name: "Homes");
        }
    }
}
