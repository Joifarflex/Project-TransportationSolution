using MediatR;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class CreateDriverHandler : IRequestHandler<CreateDriverCommand, Response>
    {
        private readonly IDriverRepository _DriverRepository;

        public CreateDriverHandler(IDriverRepository DriverRepository)
        {
            _DriverRepository = DriverRepository;
        }

        public async Task<Response> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
        {
            Response response = new();

            var driverDetails = new Driver()
            {
                driverName = command.DriverName,
                driverCode = command.DriverCode,
                driverAddress = command.DriverAddress,
            };

            //validation for duplicate unique driver data
            var getDriver = await _DriverRepository.GetDriverListAsync();

            var checkDriverCode = getDriver.Select(x => x.driverCode).Contains(command.DriverCode);
            if (checkDriverCode)
            {
                return response.BadRequest("Driver Code : " +command.DriverCode+ " is already exist!");
            }

            var checkDriverName = getDriver.Select(x => x.driverName).Contains(command.DriverName);
            if (checkDriverName)
            {
                return response.BadRequest("Driver Name : " +command.DriverName+ " is already exist");
            }

            try
            {
                await _DriverRepository.AddDriverAsync(driverDetails);
            }
            catch (Exception ex)
            {
                response.Fail("Create Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Created!");
        }
    }
}
