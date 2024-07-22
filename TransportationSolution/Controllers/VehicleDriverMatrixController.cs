using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Queries;   

namespace TransportationSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDriverMatrixController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private ILogger<VehicleDriverMatrixController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private const string VDMCacheKey = "VehicleDriverMatrixList";

        public VehicleDriverMatrixController(IMediator mediator, IMemoryCache memoryCache, ILogger<VehicleDriverMatrixController> logger)
        {
            this._mediator = mediator;
            this._memoryCache = memoryCache;
            this._logger = logger;
        }

        [HttpGet("VehicleDriverMatrixId")]
        public async Task<IActionResult> GetVehicleDriverMatrixByIdAsync(int vehicleDriverMatrixId)
        {
            if (_memoryCache.TryGetValue(VDMCacheKey, out Response<VehicleDriverMatrix>? vehicleDriverMatrixDetails))
            {
                _logger.LogInformation("Data Vehicle Driver Matrix not found in cache.");

                vehicleDriverMatrixDetails = await _mediator.Send(new GetVehicleDriverMatrixByIdQuery() { VehicleDriverMatrixId = vehicleDriverMatrixId });
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (_memoryCache.TryGetValue(VDMCacheKey, out vehicleDriverMatrixDetails))
                    {
                        _logger.LogInformation("Data Vehicle Driver Matrix found in cache.");
                    }
                    else
                    {
                        _logger.LogInformation("Data Vehicle Driver Matrix not found in cache. Fetching from database.");

                        vehicleDriverMatrixDetails = await _mediator.Send(new GetVehicleDriverMatrixByIdQuery() { VehicleDriverMatrixId = vehicleDriverMatrixId });

                        var cacheEntryOption = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1);

                        _memoryCache.Set(VDMCacheKey, vehicleDriverMatrixDetails, cacheEntryOption);
                    }
                }
                finally
                {
                    semaphore.Release();
                }

            }

            return Ok(vehicleDriverMatrixDetails);
        }

        [HttpPost]
        public async Task<Response> AddVehicleDriverMatrixAsync(VehicleDriverMatrix vdmDetails) //vdm = vehicleDriverMatrix
        {
            var vehicleDriverMatrixDetail = await _mediator.Send(new CreateVehicleDriverMatrixCommand(vdmDetails.vehicleId, vdmDetails.vehicleTypeCode, vdmDetails.licenseNumber, vdmDetails.driverId, vdmDetails.driverCode, vdmDetails.isActive, Convert.ToDateTime(vdmDetails.durationStart), Convert.ToDateTime(vdmDetails.durationEnd)));
            return vehicleDriverMatrixDetail;
        }

        [HttpPut]
        public async Task<Response> UpdateVehicleDriverMatrixAsync(VehicleDriverMatrix vdmDetails) //vdm = vehicleDriverMatrix
        {
            var isVdmDetailUpdated = await _mediator.Send(new UpdateVehicleDriverMatrixCommand(vdmDetails.vehicleDriverMatrixId, vdmDetails.vehicleId, vdmDetails.vehicleTypeCode, vdmDetails.licenseNumber, vdmDetails.driverId, vdmDetails.driverCode, vdmDetails.isActive, Convert.ToDateTime(vdmDetails.durationStart), Convert.ToDateTime(vdmDetails.durationEnd)));
            return isVdmDetailUpdated;
        }

        [HttpDelete]
        public async Task<Response> DeleteVehicleDriverMatrixAsync(int vehicleDriverMatrixId)
        {
            return await _mediator.Send(new DeleteVehicleDriverMatrixCommand() { VehicleDriverMatrixId = vehicleDriverMatrixId });
        }
    }
}
