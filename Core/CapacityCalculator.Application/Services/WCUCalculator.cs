using CapacityCalculator.Domain.Interface;

namespace CapacityCalculator.Application.Services
{
    public class WCUCalculator : IWriteCapacityCalculator
    {
        readonly int defaultSize = 1;
        public int CalculateCapacityUnits(int operationsPerSecond, decimal averageSizeOperation)
        {
            var operationSize = CalculateOperationSize(averageSizeOperation);
            return (operationsPerSecond) * (operationSize / defaultSize);

        }

        private int CalculateOperationSize(decimal operationSize)
        {
            if(operationSize < defaultSize)
                return Convert.ToInt32(defaultSize);

            if(operationSize % defaultSize != 0)
                return Convert.ToInt32(Math.Ceiling(operationSize)); 
            
            return Convert.ToInt32(operationSize.ToString()); 
        }
    }
}