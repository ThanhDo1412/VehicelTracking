using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Data.Migrations.TrackingHistory
{
    public partial class UpdateDataTypeOfLongitudeLatitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Lon",
                table: "TrackingHistories",
                type: "DECIMAL(12,9)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "TrackingHistories",
                type: "DECIMAL(12,9)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Lon",
                table: "TrackingHistories",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(12,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "TrackingHistories",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(12,9)");
        }
    }
}
