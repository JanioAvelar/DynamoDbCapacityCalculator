namespace CapacityCalculator.Domain.Interface
{
    public interface ICapacityCalculator
    {
        int CalculateCapacityUnits(int operationsPerSecond, decimal operationSize);
        
    }
}