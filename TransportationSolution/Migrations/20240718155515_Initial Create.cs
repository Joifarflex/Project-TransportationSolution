using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportationSolution.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    driverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    driverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.driverId);
                });

            migrationBuilder.InsertData(
                table: "Driver",
                columns: new[] { "driverId", "driverAddress", "driverCode", "driverName" },
                values: new object[] { 1, "PuloGadung", "AA01", "Aaron" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Driver");
        }
    }
}
