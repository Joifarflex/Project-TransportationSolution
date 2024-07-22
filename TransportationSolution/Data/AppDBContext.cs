using Microsoft.EntityFrameworkCore;
using TransportationSolution.Model;

namespace TransportationSolution.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Driver> Driver { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<VehicleDriverMatrix> VehicleDriverMatrix { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>().HasData(new Driver
            {
                driverId = 1,
                driverName = "Aaron",
                driverCode = "AA01",
                driverAddress = "PuloGadung"
            });

            modelBuilder.Entity<Vehicle>().HasData(new Vehicle
            {
                vehicleId = 1,
                vehicleTypeName = "TOYOTA Avanza 2007",
                vehicleTypeCode = "TYT01",
                licenseNumber = "BB6701C",
                year = 2007,
                isVendor = false,
            });

            modelBuilder.Entity<VehicleDriverMatrix>().HasData(new VehicleDriverMatrix
            {
                vehicleDriverMatrixId = 1,
                vehicleId = 1,
                vehicleTypeCode = "TYT01",
                licenseNumber = "BB6701C",
                driverId = 1,
                driverCode = "AA01",
                isActive = true,
                durationStart = Convert.ToDateTime("2024-08-01"),
                durationEnd = Convert.ToDateTime("2024-08-03"),
            });
        }
    }
}
