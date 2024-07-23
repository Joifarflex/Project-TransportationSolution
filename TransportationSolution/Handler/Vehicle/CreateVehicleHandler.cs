using MediatR;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleCommand, Response>
    {
        private readonly IVehicleRepository _VehicleRepository;

        public CreateVehicleHandler(IVehicleRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository;
        }
        public async Task<Response> Handle(CreateVehicleCommand command, CancellationToken cancellationToken)
        {
            Response response = new();

            var VehicleDetails = new Vehicle()
            {
                vehicleTypeName = command.VehicleTypeName,
                vehicleTypeCode = command.VehicleTypeCode,
                licenseNumber = command.LicenseNumber,
                year = command.Year,
                isVendor = command.IsVendor,
            };

            //check duplicate data vehicle
            var getListVehicle = await _VehicleRepository.GetVehicleListAsync();

            var checkVehicleTypeName = getListVehicle.Select(x => x.vehicleTypeName).Contains(command.VehicleTypeName);
            if (checkVehicleTypeName)
            {
                return response.BadRequest("Vehicle Type Name is already exist!");
            }

            var checkVehicleTypeCode = getListVehicle.Select(x => x.vehicleTypeCode).Contains(command.VehicleTypeCode);
            if (checkVehicleTypeCode)
            {
                return response.BadRequest("Vehicle Type Code is already exist!");
            }

            var checkLicenseNumber = getListVehicle.Select(x => x.licenseNumber).Contains(command.LicenseNumber);
            if (checkLicenseNumber)
            {
                return response.BadRequest("License Number is already exist!");
            }

            try
            {
                await _VehicleRepository.AddVehicleAsync(VehicleDetails);
            }
            catch (Exception ex)
            {
                return response.Fail("Create Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Created!");
        }
    }
}
