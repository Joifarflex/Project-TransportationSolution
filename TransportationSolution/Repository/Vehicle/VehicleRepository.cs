using Microsoft.EntityFrameworkCore;
using TransportationSolution.Data;
using TransportationSolution.Model;

namespace TransportationSolution.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _dbContext;

        public VehicleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicleDetails)
        {
            var result = _dbContext.Vehicle.Add(vehicleDetails);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> DeleteVehicleAsync(int vehicleId)
        {
            var filteredData = _dbContext.Vehicle.Where(x => x.vehicleId == vehicleId).FirstOrDefault();
            _dbContext.Vehicle.Remove(filteredData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            return await _dbContext.Vehicle.Where(x => x.vehicleId == vehicleId).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateVehicleAsync(Vehicle vehicleDetails)
        {
            _dbContext.Vehicle.Update(vehicleDetails);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetVehicleListAsync()
        {
            return await _dbContext.Vehicle.ToListAsync();
        }
    }
}
