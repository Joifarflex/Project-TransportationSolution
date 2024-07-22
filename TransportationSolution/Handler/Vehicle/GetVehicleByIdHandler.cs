using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Queries;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, Response<Vehicle>>
    {
        private readonly IVehicleRepository _VehicleRepository;

        public GetVehicleByIdHandler(IVehicleRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository;
        }

        public async Task<Response<Vehicle>> Handle(GetVehicleByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<Vehicle>();

            var getVehicle = await _VehicleRepository.GetVehicleByIdAsync(query.VehicleId);

            if (getVehicle == null)
            {
                return response.BadRequest("Data Vehicle is not found.");
            }

            return response.Success("Data has Found", getVehicle);
        }
    }
}
