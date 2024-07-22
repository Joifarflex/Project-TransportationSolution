using TransportationSolution.Common;
using MediatR;
using TransportationSolution.Repository;
using TransportationSolution.Command;
using Microsoft.IdentityModel.Tokens;

namespace TransportationSolution.Handler
{
    public class UpdateVehicleDriverMatrixHandler : IRequestHandler<UpdateVehicleDriverMatrixCommand, Response>
    {
        private readonly IVehicleDriverMatrixRepository _vehicleDriverMatrixRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public UpdateVehicleDriverMatrixHandler(IVehicleDriverMatrixRepository vehicleDriverMatrixRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            _vehicleDriverMatrixRepository = vehicleDriverMatrixRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Response> Handle(UpdateVehicleDriverMatrixCommand command, CancellationToken cancellationToken)
        {
            Response response = new();
            var getVDM = await _vehicleDriverMatrixRepository.GetVehicleDriverMatrixByIdAsync(command.VehicleDriverMatrixId);

            //vdm = vehicleDriverMatrix
            if (getVDM == null)
            {
                return response.BadRequest("Data Vehicle Driver Matrix is not Found!");
            }

            getVDM.vehicleDriverMatrixId = command.VehicleDriverMatrixId;
            getVDM.vehicleId = command.VehicleId;
            getVDM.vehicleTypeCode = command.VehicleTypeCode;
            getVDM.licenseNumber = command.LicenseNumber;
            getVDM.driverId = command.DriverId;
            getVDM.driverCode = command.DriverCode;
            getVDM.isActive = command.IsActive;
            getVDM.durationStart = command.DurationStart;
            getVDM.durationEnd = command.DurationEnd;

            //validate if every vehicle data has exist in master data
            var getVehicleData = await _vehicleRepository.GetVehicleListAsync();

            var checkExistVehicle = getVehicleData.Where(x => x.vehicleId == command.VehicleId).ToList();
            if (checkExistVehicle.IsNullOrEmpty())
            {
                return response.BadRequest("Data vehicle is not exist.");
            }

            var vehicleCode = checkExistVehicle.Select(x => x.vehicleTypeCode == command.VehicleTypeCode).FirstOrDefault();
            if (!vehicleCode)
            {
                return response.BadRequest("Vehicle Type Code : " + command.VehicleTypeCode + " is not exist.");
            }

            var licenseNumber = checkExistVehicle.Select(x => x.licenseNumber == command.LicenseNumber).FirstOrDefault();
            if (!licenseNumber)
            {
                return response.BadRequest("License Number : " + command.LicenseNumber + " is not exist anymore.");
            }

            //validate if every driver data has exist in master data
            var getDriverData = await _driverRepository.GetDriverListAsync();

            var checkExistDriver = getDriverData.Where(x => x.driverId == command.DriverId).ToList();
            if (checkExistDriver.IsNullOrEmpty())
            {
                return response.BadRequest("Data driver is not exist.");
            }

            var driverCode = checkExistDriver.Select(x => x.driverCode == command.DriverCode).FirstOrDefault();
            if (!driverCode)
            {
                return response.BadRequest("Driver Code : " + command.DriverCode + " is not exist anymore.");
            }

            try
            {
                await _vehicleDriverMatrixRepository.UpdateVehicleDriverMatrixAsync(getVDM);
            }
            catch (Exception ex)
            {
                return response.Fail("Update Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Updated!");
        }
    }
}
