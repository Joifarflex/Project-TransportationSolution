using MediatR;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, Response>
    {
        private readonly IVehicleRepository _VehicleRepository;

        public DeleteVehicleHandler(IVehicleRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository;
        }

        public async Task<Response> Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
        {
            Response response = new();
            var VehicleDetails = await _VehicleRepository.GetVehicleByIdAsync(command.VehicleId);
            if (VehicleDetails == null)
            {
                return response.BadRequest("Data Vehicle is not found.");
            }

            try
            {
                await _VehicleRepository.DeleteVehicleAsync(VehicleDetails.vehicleId);
            }
            catch (Exception ex)
            {
                response.Fail("Deleted Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Deleted!");
        }
    }
}
