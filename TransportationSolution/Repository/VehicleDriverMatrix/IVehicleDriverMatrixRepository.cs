using TransportationSolution.Model;

namespace TransportationSolution.Repository
{
    public interface IVehicleDriverMatrixRepository
    {
        public Task<List<VehicleDriverMatrix>> GetVehicleDriverMatrixListAsync();
        public Task<VehicleDriverMatrix> GetVehicleDriverMatrixByIdAsync(int Id);
        public Task<VehicleDriverMatrix> AddVehicleDriverMatrixAsync(VehicleDriverMatrix vdmDetails);
        public Task<int> UpdateVehicleDriverMatrixAsync(VehicleDriverMatrix vdmDetails);
        public Task<int> DeleteVehicleDriverMatrixAsync(int Id);
    }
}
