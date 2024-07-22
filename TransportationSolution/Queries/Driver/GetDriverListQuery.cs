using MediatR;
using TransportationSolution.Model;

namespace TransportationSolution.Queries
{
    public class GetDriverListQuery : IRequest<List<Driver>>
    {
    }
}
