using TransportationSolution.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TransportationSolution.Command
{
    public class UpdateVehicleCommand : IRequest<Response>
    {
        public int VehicleId { get; set; }
        public string VehicleTypeName { get; set; }
        public string VehicleTypeCode { get; set; }
        public string LicenseNumber { get; set; }
        public int Year { get; set; }
        public bool IsVendor { get; set; }

        public UpdateVehicleCommand(int vehicleId, string vehicleTypeName, string vehicleTypeCode, string licenseNumber, int year, bool isVendor)
        {
            VehicleId = vehicleId;
            VehicleTypeName = vehicleTypeName;
            VehicleTypeCode = vehicleTypeCode;
            LicenseNumber = licenseNumber;
            Year = year;
            IsVendor = isVendor;
        }
    }
}
