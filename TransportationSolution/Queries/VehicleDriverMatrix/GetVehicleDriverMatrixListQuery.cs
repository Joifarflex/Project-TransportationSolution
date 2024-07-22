using MediatR;
using TransportationSolution.Model;

namespace TransportationSolution.Queries
{
    public class GetVehicleDriverMatrixListQuery : IRequest<List<VehicleDriverMatrix>>
    {
    }
}
