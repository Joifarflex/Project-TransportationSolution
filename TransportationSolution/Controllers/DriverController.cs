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
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private ILogger<DriverController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private const string DriverCacheKey = "DriverList";

        public DriverController(IMediator _mediator, IMemoryCache memoryCache, ILogger<DriverController> logger)
        {
            this._mediator = _mediator;
            this._memoryCache = memoryCache;
            this._logger = logger;
        }

        [HttpGet("")]
        public async Task<List<Driver>> GetDriverListAsync()
        {
            var DriverDetails = await _mediator.Send(new GetDriverListQuery());
            return DriverDetails;

        }

        [HttpGet("DriverId")]
        public async Task<IActionResult> GetDriverByIdAsync(int driverId)
        {
            if (_memoryCache.TryGetValue(DriverCacheKey, out Response<Driver>? DriverDetails))
            {
                _logger.LogInformation("Data Driver found in cache.");

                DriverDetails = await _mediator.Send(new GetDriverByIdQuery() { DriverId = driverId });
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (_memoryCache.TryGetValue(DriverCacheKey, out DriverDetails))
                    {
                        _logger.LogInformation("Data Driver found in cache.");
                    }
                    else
                    {
                        _logger.LogInformation("Data Driver not found in cache. Fetching from database.");

                        DriverDetails = await _mediator.Send(new GetDriverByIdQuery() { DriverId = driverId });

                        var cacheEntryOption = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1);

                        _memoryCache.Set(DriverCacheKey, DriverDetails, cacheEntryOption);
                    }
                }
                finally
                {
                    semaphore.Release();
                }

            }

            return Ok(DriverDetails);
        }

        [HttpPost]
        public async Task<Response> AddDriverAsync(Driver driverDetails)
        {
            var DriverDetail = await _mediator.Send(new CreateDriverCommand(driverDetails.driverName, driverDetails.driverCode, driverDetails.driverAddress));
            return DriverDetail;
        }

        [HttpPut]
        public async Task<Response> UpdateDriverAsync(Driver driverDetails)
        {
            var isDriverDetailUpdated = await _mediator.Send(new UpdateDriverCommand(driverDetails.driverId, driverDetails.driverName, driverDetails.driverCode, driverDetails.driverAddress));
            return isDriverDetailUpdated;
        }

        [HttpDelete]
        public async Task<Response> DeleteDriverAsync(int driverId)
        {
            return await _mediator.Send(new DeleteDriverCommand() { DriverId = driverId });
        }
    }
}
