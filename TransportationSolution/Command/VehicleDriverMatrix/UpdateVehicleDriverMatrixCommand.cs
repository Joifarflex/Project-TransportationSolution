using MediatR;
using TransportationSolution.Common;
using TransportationSolution.Model;

namespace TransportationSolution.Command
{
    public class UpdateVehicleDriverMatrixCommand : IRequest<Response>
    {
        public int VehicleDriverMatrixId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleTypeCode { get; set; }
        public string LicenseNumber { get; set; }
        public int DriverId { get; set; }
        public string DriverCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime DurationStart { get; set; }
        public DateTime DurationEnd { get; set; }

        public UpdateVehicleDriverMatrixCommand(int vehicleDriverMatrixId, int vehicleId, string vehicleTypeCode, string licenseNumber, int driverId, string driverCode, bool isActive, DateTime durationStart, DateTime durationEnd)
        {
            VehicleDriverMatrixId = vehicleDriverMatrixId;
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
