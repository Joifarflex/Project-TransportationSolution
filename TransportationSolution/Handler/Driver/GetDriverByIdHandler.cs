using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Queries;
using TransportationSolution.Repository;

namespace TransportationSolution.Handler
{
    public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdQuery, Response<Driver>>
    {
        private readonly IDriverRepository _DriverRepository;

        public GetDriverByIdHandler(IDriverRepository DriverRepository)
        {
            _DriverRepository = DriverRepository;
        }

        public async Task<Response<Driver>> Handle(GetDriverByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<Driver>();

            var getDriver = await _DriverRepository.GetDriverByIdAsync(query.DriverId);

            if (getDriver == null)
            {
                return response.BadRequest("Data Driver is not found.");
            }

            return response.Success("Data has Found", getDriver);
        }
    }
}
