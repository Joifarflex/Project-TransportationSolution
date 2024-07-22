using Microsoft.EntityFrameworkCore;
using TransportationSolution.Data;
using TransportationSolution.Model;

namespace TransportationSolution.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _dbContext;

        public DriverRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Driver> AddDriverAsync(Driver driverDetails)
        {
            var result = _dbContext.Driver.Add(driverDetails);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> DeleteDriverAsync(int driverId)
        {
            var filteredData = _dbContext.Driver.Where(x => x.driverId == driverId).FirstOrDefault();
            _dbContext.Driver.Remove(filteredData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(int driverId)
        {
            return await _dbContext.Driver.Where(x => x.driverId == driverId).FirstOrDefaultAsync();
        }

        public async Task<List<Driver>> GetDriverListAsync()
        {
            return await _dbContext.Driver.ToListAsync();
        }

        public async Task<int> UpdateDriverAsync(Driver driverDetails)
        {
            _dbContext.Driver.Update(driverDetails);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
