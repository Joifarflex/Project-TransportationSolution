using MediatR;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class DeleteVehicleDriverMatrixHandler : IRequestHandler<DeleteVehicleDriverMatrixCommand, Response>
    {
        private readonly IVehicleDriverMatrixRepository _vehicleDriverMatrixRepository;

        public DeleteVehicleDriverMatrixHandler(IVehicleDriverMatrixRepository vehicleDriverMatrixRepository)
        {
            _vehicleDriverMatrixRepository = vehicleDriverMatrixRepository;
        }
        public async Task<Response> Handle(DeleteVehicleDriverMatrixCommand command, CancellationToken cancellationToken)
        {
            Response response = new();
            var vehicleDriverMatrix = await _vehicleDriverMatrixRepository.GetVehicleDriverMatrixByIdAsync(command.VehicleDriverMatrixId);
            if (vehicleDriverMatrix == null)
            {
                return response.BadRequest("Data Vehicle Driver Matrix is not found!");
            }

            try
            {
                await _vehicleDriverMatrixRepository.DeleteVehicleDriverMatrixAsync(vehicleDriverMatrix.vehicleDriverMatrixId);
            }
            catch (Exception ex)
            {
                return response.Fail("Deleted Data has Failed - " + ex);
            }

            return response.Success("Data is Successfully Deleted!");
        }
    }
}
