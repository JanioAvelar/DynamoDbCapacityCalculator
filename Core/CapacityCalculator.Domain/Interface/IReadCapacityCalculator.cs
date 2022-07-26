namespace CapacityCalculator.Domain.Interface
{
    public interface IReadCapacityCalculator
    {
        int CalculateStronglyConsistentCapacityUnits(int operationsPerSecond, int averageSize);
        int CalculateEventuallyConsistentCapacityUnits(int operationsPerSecond, int averageSize);
    }
}