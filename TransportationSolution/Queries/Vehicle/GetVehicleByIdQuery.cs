using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;

namespace TransportationSolution.Queries
{
    public class GetVehicleByIdQuery : IRequest<Response<Vehicle>>
    {
        public int VehicleId { get; set; }
    }
}
