using TransportationSolution.Common;
using MediatR;
using TransportationSolution.Command;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class UpdateDriverHandler : IRequestHandler<UpdateDriverCommand, Response>
    {
        private readonly IDriverRepository _DriverRepository;

        public UpdateDriverHandler(IDriverRepository DriverRepository)
        {
            _DriverRepository = DriverRepository;
        }
        public async Task<Response> Handle(UpdateDriverCommand command, CancellationToken cancellationToken)
        {
            Response response = new();
            var DriverDetails = await _DriverRepository.GetDriverByIdAsync(command.DriverId);

            if (DriverDetails == null)
                return response.BadRequest("Data Driver is not Found!");

            //validation for duplicate driver data
            var getDriver = await _DriverRepository.GetDriverListAsync();

            var query = getDriver.Where(x => x.driverId != command.DriverId).ToList();

            if (query.Select(x => x.driverCode).Contains(command.DriverCode))
            {
                return response.BadRequest("Driver code : " + command.DriverCode + " is already exist!");
            }

            if (query.Select(x => x.driverName).Contains(command.DriverName))
            {
                return response.BadRequest("Driver Name : " + command.DriverName + " is already exist!");
            }

            DriverDetails.driverId = command.DriverId;
            DriverDetails.driverName = command.DriverName;
            DriverDetails.driverCode = command.DriverCode;
            DriverDetails.driverAddress = command.DriverAddress;

            try
            {
                await _DriverRepository.UpdateDriverAsync(DriverDetails);
            }
            catch (Exception ex)
            {
                response.Fail("Update Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Updated!");
        }
    }
}
