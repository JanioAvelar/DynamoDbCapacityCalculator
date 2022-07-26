namespace CapacityCalculator.API.Models
{
    public class CalculateReadCapacityModel
    {
        public int OperationsPerSecond { get; set; }
        public int AverageSize { get; set; }
    }
}