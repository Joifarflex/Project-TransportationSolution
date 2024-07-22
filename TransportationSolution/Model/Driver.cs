using System.ComponentModel.DataAnnotations;

namespace TransportationSolution.Model
{
    public class Driver
    {
        [Key]
        public int driverId { get; set; }
        public string driverName { get; set; }
        public string driverCode { get; set; }
        public string driverAddress { get; set; }
    }
}
