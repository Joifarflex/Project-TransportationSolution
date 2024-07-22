using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportationSolution.Common;
using TransportationSolution.Model;

namespace TransportationSolution.Command
{
    public class CreateDriverCommand : IRequest<Response>
    {
        public string DriverName { get; set; }
        public string DriverCode { get; set; }
        public string DriverAddress { get; set; }

        public CreateDriverCommand(string driverName, string driverCode, string driverAddress)
        {
            DriverName = driverName;
            DriverCode = driverCode;
            DriverAddress = driverAddress;
        }
    }
}
