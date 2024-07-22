using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using TransportationSolution.Command;
using TransportationSolution.Common;
using TransportationSolution.Model;
using TransportationSolution.Queries;

namespace TransportationSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private ILogger<VehicleController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private const string VehicleCacheKey = "VehicleList";

        public VehicleController(IMediator _mediator, IMemoryCache memoryCache, ILogger<VehicleController> logger)
        {
            this._mediator = _mediator;
            this._memoryCache = memoryCache;
            this._logger = logger;
        }

        [HttpGet("VehicleId")]
        public async Task<IActionResult> GetVehicleByIdAsync(int vehicleId)
        {
            if (_memoryCache.TryGetValue(VehicleCacheKey, out Response<Vehicle>? VehicleDetails))
            {
                _logger.LogInformation("Data Vehicle found in cache.");

                VehicleDetails = await _mediator.Send(new GetVehicleByIdQuery() { VehicleId = vehicleId });
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (_memoryCache.TryGetValue(VehicleCacheKey, out VehicleDetails))
                    {
                        _logger.LogInformation("Data Vehicle found in cache.");
                    }
                    else
                    {
                        _logger.LogInformation("Data Vehicle not found in cache. Fetching from database.");

                        VehicleDetails = await _mediator.Send(new GetVehicleByIdQuery() { VehicleId = vehicleId });

                        var cacheEntryOption = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1);

                        _memoryCache.Set(VehicleCacheKey, VehicleDetails, cacheEntryOption);
                    }
                }
                finally
                {
                    semaphore.Release();
                }

            }

            return Ok(VehicleDetails);
        }

        [HttpPost]
        public async Task<Response> AddVehicleAsync(Vehicle vehicleDetails)
        {
            var VehicleDetail = await _mediator.Send(new CreateVehicleCommand(vehicleDetails.vehicleTypeName, vehicleDetails.vehicleTypeCode, vehicleDetails.licenseNumber, vehicleDetails.year, vehicleDetails.isVendor));
            return VehicleDetail;
        }

        [HttpPut]
        public async Task<Response> UpdateVehicleAsync(Vehicle vehicleDetails)
        {
            var isVehicleDetailUpdated = await _mediator.Send(new UpdateVehicleCommand(vehicleDetails.vehicleId, vehicleDetails.vehicleTypeName, vehicleDetails.vehicleTypeCode, vehicleDetails.licenseNumber, vehicleDetails.year, vehicleDetails.isVendor));
            return isVehicleDetailUpdated;
        }

        [HttpDelete]
        public async Task<Response> DeleteVehicleAsync(int vehicleId)
        {
            return await _mediator.Send(new DeleteVehicleCommand() { VehicleId = vehicleId });
        }
    }
}
