using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VT.Data.Migrations.TrackingHistory
{
    public partial class initTrackingHistoryContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TrackingRemark = table.Column<string>(nullable: true),
                    VehicleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackingHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TrackingSessionId = table.Column<Guid>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Lon = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingHistories_TrackingSessions_TrackingSessionId",
                        column: x => x.TrackingSessionId,
                        principalTable: "TrackingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackingHistories_TrackingSessionId",
                table: "TrackingHistories",
                column: "TrackingSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingHistories");

            migrationBuilder.DropTable(
                name: "TrackingSessions");
        }
    }
}
