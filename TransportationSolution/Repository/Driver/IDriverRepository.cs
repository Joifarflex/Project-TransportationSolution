using TransportationSolution.Model;

namespace TransportationSolution.Repository
{
    public interface IDriverRepository
    {
        public Task<List<Driver>> GetDriverListAsync();
        public Task<Driver> GetDriverByIdAsync(int Id);
        public Task<Driver> AddDriverAsync(Driver driverDetails);
        public Task<int> UpdateDriverAsync(Driver driverDetails);
        public Task<int> DeleteDriverAsync(int Id);
    }
}
