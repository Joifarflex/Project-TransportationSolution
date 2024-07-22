using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;

namespace TransportationSolution.Queries
{
    public class GetDriverByIdQuery : IRequest<Response<Driver>>
    {
        public int DriverId { get; set; }
    }
}
