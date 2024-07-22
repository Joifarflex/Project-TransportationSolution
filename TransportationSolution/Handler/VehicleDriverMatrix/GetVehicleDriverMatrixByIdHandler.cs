using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Queries;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class GetVehicleDriverMatrixByIdHandler : IRequestHandler<GetVehicleDriverMatrixByIdQuery, Response<VehicleDriverMatrix>>
    {
        private readonly IVehicleDriverMatrixRepository _vehicleDriverMatrixRepository;

        public GetVehicleDriverMatrixByIdHandler(IVehicleDriverMatrixRepository vehicleDriverMatrixRepository)
        {
            _vehicleDriverMatrixRepository = vehicleDriverMatrixRepository;
        }
        public async Task<Response<VehicleDriverMatrix>> Handle(GetVehicleDriverMatrixByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<VehicleDriverMatrix>();

            var getVehicleDriverMatrix = await _vehicleDriverMatrixRepository.GetVehicleDriverMatrixByIdAsync(query.VehicleDriverMatrixId);

            if (getVehicleDriverMatrix == null)
            {
                return response.BadRequest("Data Vehicle Driver Matrix is not found");
            }

            return response.Success("Data has Found", getVehicleDriverMatrix);
        }
    }
}
