using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;

namespace TransportationSolution.Queries
{
    public class GetVehicleDriverMatrixByIdQuery : IRequest<Response<VehicleDriverMatrix>>
    {
        public int VehicleDriverMatrixId { get; set; }
    }
}
