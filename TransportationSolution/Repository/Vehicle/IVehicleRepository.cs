using TransportationSolution.Model;

namespace TransportationSolution.Repository
{
    public interface IVehicleRepository
    {
        public Task<List<Vehicle>> GetVehicleListAsync();
        public Task<Vehicle> GetVehicleByIdAsync(int Id);
        public Task<Vehicle> AddVehicleAsync(Vehicle vehicleDetails);
        public Task<int> UpdateVehicleAsync(Vehicle vehicleDetails);
        public Task<int> DeleteVehicleAsync(int Id);
    }
}
