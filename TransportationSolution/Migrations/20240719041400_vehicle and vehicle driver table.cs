using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportationSolution.Migrations
{
    /// <inheritdoc />
    public partial class vehicleandvehicledrivertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Driver",
                keyColumn: "driverId",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    vehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vehicleTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vehicleTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    licenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    isVendor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.vehicleId);
                });

            migrationBuilder.CreateTable(
                name: "vehicleDriverMatrix",
                columns: table => new
                {
                    vehicleDriverMatrixId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vehicleId = table.Column<int>(type: "int", nullable: false),
                    licenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vehicleTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverId = table.Column<int>(type: "int", nullable: false),
                    driverCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    durationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    durationEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicleDriverMatrix", x => x.vehicleDriverMatrixId);
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "vehicleId", "isVendor", "licenseNumber", "vehicleTypeCode", "vehicleTypeName", "year" },
                values: new object[] { 1, false, "BB6701C", "TYT01", "TOYOTA Avanza 2007", 2007 });

            migrationBuilder.InsertData(
                table: "vehicleDriverMatrix",
                columns: new[] { "vehicleDriverMatrixId", "driverCode", "driverId", "durationEnd", "durationStart", "isActive", "licenseNumber", "vehicleId", "vehicleTypeCode" },
                values: new object[] { 1, "AA01", 1, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "BB6701C", 1, "TYT01" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "vehicleDriverMatrix");

            migrationBuilder.InsertData(
                table: "Driver",
                columns: new[] { "driverId", "driverAddress", "driverCode", "driverName" },
                values: new object[] { 1, "PuloGadung", "AA01", "Aaron" });
        }
    }
}
