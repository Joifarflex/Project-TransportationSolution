using TransportationSolution.Common;
using MediatR;
using TransportationSolution.Repository;
using TransportationSolution.Command;

namespace TransportationSolution.Handler
{
    public class UpdatevehicleHandler : IRequestHandler<UpdateVehicleCommand, Response>
    {
        private readonly IVehicleRepository _VehicleRepository;

        public UpdatevehicleHandler(IVehicleRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository;
        }
        public async Task<Response> Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
        {
            Response response = new();
            var VehicleDetails = await _VehicleRepository.GetVehicleByIdAsync(command.VehicleId);

            if (VehicleDetails == null)
                return response.BadRequest("Data Vehicle is not Found!");

            //validation for duplicate vehicle data
            var getListVehicle = await _VehicleRepository.GetVehicleListAsync();

            var queryAllVehicle = getListVehicle.Where(x => x.vehicleId != command.VehicleId).ToList();

            var queryVehicleCode = queryAllVehicle.Select(x => x.vehicleTypeCode).Contains(command.VehicleTypeCode);
            if (queryVehicleCode)
            {
                return response.BadRequest("Vehicle Type Code : " +command.VehicleTypeCode+ " is already exist!");
            }

            var queryVehicleName = queryAllVehicle.Select(x => x.vehicleTypeName).Contains(command.VehicleTypeName);
            if (queryVehicleName)
            {
                return response.BadRequest("Vehicle Type Name : " + command.VehicleTypeName + " is already exist!");
            }

            var licenseCheck = queryAllVehicle.Select(x => x.licenseNumber).Contains(command.LicenseNumber);
            if (licenseCheck)
            {
                return response.BadRequest("License Number : " + command.LicenseNumber + " is already exist");
            }

            VehicleDetails.vehicleId = command.VehicleId;
            VehicleDetails.vehicleTypeName = command.VehicleTypeName;
            VehicleDetails.vehicleTypeCode = command.VehicleTypeCode;
            VehicleDetails.licenseNumber = command.LicenseNumber;
            VehicleDetails.year = command.Year;
            VehicleDetails.isVendor = command.IsVendor;

            try
            {
                await _VehicleRepository.UpdateVehicleAsync(VehicleDetails);
            }
            catch (Exception ex)
            {
                return response.Fail("Update Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Updated!");
        }
    }
}
