using MediatR;
using TransportationSolution.Common;

namespace TransportationSolution.Command
{
    public class DeleteDriverCommand : IRequest<Response>
    {
        public int DriverId { get; set; }
    }
}
