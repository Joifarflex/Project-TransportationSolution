using Microsoft.EntityFrameworkCore;
using TransportationSolution.Data;
using TransportationSolution.Model;

namespace TransportationSolution.Repository
{
    public class VehicleDriverMatrixRepository : IVehicleDriverMatrixRepository
    {
        private readonly AppDbContext _dbContext;

        public VehicleDriverMatrixRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VehicleDriverMatrix> AddVehicleDriverMatrixAsync(VehicleDriverMatrix vdmDetails)
        {
            var result = _dbContext.VehicleDriverMatrix.Add(vdmDetails);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> DeleteVehicleDriverMatrixAsync(int vehicleDriverMatrixId)
        {
            var filteredData = _dbContext.VehicleDriverMatrix.Where(x => x.vehicleDriverMatrixId == vehicleDriverMatrixId).FirstOrDefault();
            _dbContext.VehicleDriverMatrix.Remove(filteredData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<VehicleDriverMatrix> GetVehicleDriverMatrixByIdAsync(int vehicleDriverMatrixId)
        {
            return await _dbContext.VehicleDriverMatrix.Where(x => x.vehicleDriverMatrixId == vehicleDriverMatrixId).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateVehicleDriverMatrixAsync(VehicleDriverMatrix vdmDetails)
        {
            _dbContext.VehicleDriverMatrix.Update(vdmDetails);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<VehicleDriverMatrix>> GetVehicleDriverMatrixListAsync()
        {
            return await _dbContext.VehicleDriverMatrix.ToListAsync();
        }
    }
}
