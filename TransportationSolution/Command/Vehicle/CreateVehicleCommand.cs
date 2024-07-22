using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportationSolution.Common;
using TransportationSolution.Model;

namespace TransportationSolution.Command
{
    public class CreateVehicleCommand : IRequest<Response>
    {
        public string VehicleTypeName { get; set; }
        public string VehicleTypeCode { get; set; }
        public string LicenseNumber { get; set; }
        public int Year { get; set; }
        public bool IsVendor { get; set; }

        public CreateVehicleCommand(string vehicleTypeName, string vehicleTypeCode, string licenseNumber, int year, bool isVendor)
        {
            VehicleTypeName = vehicleTypeName;
            VehicleTypeCode = vehicleTypeCode;
            LicenseNumber = licenseNumber;
            Year = year;
            IsVendor = isVendor;
        }
    }
}
