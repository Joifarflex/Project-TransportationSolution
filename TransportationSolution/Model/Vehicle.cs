using System.ComponentModel.DataAnnotations;

namespace TransportationSolution.Model
{
    public class Vehicle
    {
        [Key]
        public int vehicleId { get; set; }
        public string vehicleTypeName { get; set; }
        public string vehicleTypeCode { get; set; }
        public string licenseNumber { get; set; }
        public int year { get; set; }
        public bool isVendor { get; set; } = false;
    }
}
