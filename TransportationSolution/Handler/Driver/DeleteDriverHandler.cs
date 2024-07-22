using MediatR;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class DeleteDriverHandler : IRequestHandler<DeleteDriverCommand, Response>
    {
        private readonly IDriverRepository _DriverRepository;

        public DeleteDriverHandler(IDriverRepository DriverRepository)
        {
            _DriverRepository = DriverRepository;
        }

        public async Task<Response> Handle(DeleteDriverCommand command, CancellationToken cancellationToken)
        {
            Response response = new();
            var DriverDetails = await _DriverRepository.GetDriverByIdAsync(command.DriverId);
            if (DriverDetails == null)
            {
                return response.BadRequest("Data Driver is not found!");
            }

            try
            {
                await _DriverRepository.DeleteDriverAsync(DriverDetails.driverId);
            }
            catch (Exception ex)
            {
                response.Fail("Deleted Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Deleted!");
        }
    }
}
