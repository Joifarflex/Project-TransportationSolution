using MediatR;
using TransportationSolution.Common;

namespace TransportationSolution.Command
{
    public class DeleteVehicleCommand : IRequest<Response>
    {
        public int VehicleId { get; set; }
    }
}
