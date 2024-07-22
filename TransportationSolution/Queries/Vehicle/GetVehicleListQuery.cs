using MediatR;
using TransportationSolution.Model;

namespace TransportationSolution.Queries
{
    public class GetVehicleListQuery : IRequest<List<Vehicle>>
    {
    }
}
