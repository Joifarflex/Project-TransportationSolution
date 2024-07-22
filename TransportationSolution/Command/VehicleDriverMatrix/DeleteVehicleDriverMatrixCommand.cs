using MediatR;
using TransportationSolution.Common;

namespace TransportationSolution.Command
{
    public class DeleteVehicleDriverMatrixCommand : IRequest<Response>
    {
        public int VehicleDriverMatrixId { get; set; }
    }
}
