using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_Zone.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class ParkingZoneEditedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingZones_Addresses_address_id",
                table: "ParkingZones");

            migrationBuilder.RenameColumn(
                name: "address_id",
                table: "ParkingZones",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingZones_address_id",
                table: "ParkingZones",
                newName: "IX_ParkingZones_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingZones_Addresses_AddressId",
                table: "ParkingZones",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingZones_Addresses_AddressId",
                table: "ParkingZones");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "ParkingZones",
                newName: "address_id");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingZones_AddressId",
                table: "ParkingZones",
                newName: "IX_ParkingZones_address_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingZones_Addresses_address_id",
                table: "ParkingZones",
                column: "address_id",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
