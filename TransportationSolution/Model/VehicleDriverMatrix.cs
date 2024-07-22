using System.ComponentModel.DataAnnotations;

namespace TransportationSolution.Model
{
    public class VehicleDriverMatrix
    {
        [Key]
        public int vehicleDriverMatrixId { get; set; }
        public int vehicleId { get; set; }
        public string licenseNumber { get; set; }
        public string vehicleTypeCode { get; set; }
        public int driverId { get; set; }
        public string driverCode { get; set; }
        public bool isActive { get; set; }
        public DateTime durationStart { get; set; }
        public DateTime durationEnd { get; set; }
    }
}
