using TransportationSolution.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TransportationSolution.Command
{
    public class UpdateDriverCommand : IRequest<Response>
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverCode { get; set; }
        public string DriverAddress { get; set; }

        public UpdateDriverCommand(int driverId, string driverName, string driverCode, string driverAddress)
        {
            DriverId = driverId;
            DriverName = driverName;
            DriverCode = driverCode;
            DriverAddress = driverAddress;
        }
    }
}
