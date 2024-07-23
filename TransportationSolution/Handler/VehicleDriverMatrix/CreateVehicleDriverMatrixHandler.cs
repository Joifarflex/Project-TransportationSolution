using MediatR;
using Microsoft.IdentityModel.Tokens;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class CreateVehicleDriverMatrixHandler : IRequestHandler<CreateVehicleDriverMatrixCommand, Response>
    {
        private readonly IVehicleDriverMatrixRepository _vehicleDriverMatrixRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CreateVehicleDriverMatrixHandler(IVehicleDriverMatrixRepository vehicleDriverMatrixRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            _vehicleDriverMatrixRepository = vehicleDriverMatrixRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Response> Handle(CreateVehicleDriverMatrixCommand command, CancellationToken cancellationToken)
        {
            Response response = new();

            var vehicleDriverMatrix = new VehicleDriverMatrix()
            {
                vehicleId = command.VehicleId,
                vehicleTypeCode = command.VehicleTypeCode,
                licenseNumber = command.LicenseNumber,
                driverId = command.DriverId,
                driverCode = command.DriverCode,
                isActive = command.IsActive,
                durationStart = command.DurationStart,
                durationEnd = command.DurationEnd,
            };

            //check if vehicle or driver still have active status
            var getListData = await _vehicleDriverMatrixRepository.GetVehicleDriverMatrixListAsync();

            var checkStatus = getListData.Where(x => x.vehicleId == command.VehicleId && x.driverId == command.DriverId).Select(x => x.isActive).ToList();

            if (checkStatus.Exists(x => x))
            {
                return response.BadRequest("Data vehicle and driver still in active duration");
            }

            //validate if every vehicle data has exist in master data
            var getVehicleData = await _vehicleRepository.GetVehicleListAsync();

            var checkExistVehicle = getVehicleData.Where(x => x.vehicleId == command.VehicleId).ToList();
            if (checkExistVehicle.IsNullOrEmpty())
            {
                return response.BadRequest("Data vehicle is not exist.");
            }

            var vehicleCode = checkExistVehicle.Select(x => x.vehicleTypeCode).Contains(command.VehicleTypeCode);
            if (!vehicleCode)
            {
                return response.BadRequest("Vehicle Type Code : " + command.VehicleTypeCode + " is not exist.");
            }

            var licenseNumber = checkExistVehicle.Select(x => x.licenseNumber).Contains(command.LicenseNumber);
            if (!licenseNumber)
            {
                return response.BadRequest("License Number : " + command.LicenseNumber + " is not exist.");
            }

            //validate if every driver data has exist in master data
            var getDriverData = await _driverRepository.GetDriverListAsync();

            var checkExistDriver = getDriverData.Where(x => x.driverId == command.DriverId).ToList();
            if (checkExistDriver.IsNullOrEmpty())
            {
                return response.BadRequest("Data driver is not exist.");
            }

            var driverCode = checkExistDriver.Select(x => x.driverCode).Contains(command.DriverCode);
            if (!driverCode)
            {
                return response.BadRequest("Driver Code : " + command.DriverCode + " is not exist anymore.");
            }

            try
            {
                await _vehicleDriverMatrixRepository.AddVehicleDriverMatrixAsync(vehicleDriverMatrix);
            }
            catch (Exception ex)
            {
                return response.Fail("Create Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Created!");
        }
    }
}
