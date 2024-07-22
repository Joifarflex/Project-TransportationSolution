using MediatR;
using TransportationSolution.Common;

namespace TransportationSolution.Command
{
    public class CreateVehicleDriverMatrixCommand : IRequest<Response>
    {
        public int VehicleId { get; set; }
        public string VehicleTypeCode { get; set; }
        public string LicenseNumber { get; set; }
        public int DriverId { get; set; }
        public string DriverCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime DurationStart { get; set; }
        public DateTime DurationEnd { get; set; }

        public CreateVehicleDriverMatrixCommand(int vehicleId, string vehicleTypeCode, string licenseNumber, int driverId, string driverCode, bool isActive, DateTime durationStart, DateTime durationEnd)
        {
            VehicleId = vehicleId;
            VehicleTypeCode = vehicleTypeCode;
            LicenseNumber = licenseNumber;
            DriverId = driverId;
            DriverCode = driverCode;
            IsActive = isActive;
            DurationStart = durationStart;
            DurationEnd = durationEnd;
        }
    }
}
