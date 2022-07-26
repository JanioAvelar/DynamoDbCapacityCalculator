using CapacityCalculator.Domain.Interface;

namespace CapacityCalculator.Application.Services
{
    public class RCUCalculator : IReadCapacityCalculator
    {
        readonly int minimumSize = 4;
        
        public int CalculateEventuallyConsistentCapacityUnits(
            int operationsPerSecond, 
            int averageSize)
        {
            ValidateInputs(operationsPerSecond);
            
            int _operationSize = CalculateOperationSize(averageSize); 

            return (operationsPerSecond / 2) * (_operationSize / minimumSize);
        }

        public int CalculateStronglyConsistentCapacityUnits(
            int operationsPerSecond, 
            int operationSize)
        {
            ValidateInputs(operationsPerSecond);

            int _operationSize = CalculateOperationSize(operationSize); 
            
            return (operationsPerSecond) * (_operationSize / minimumSize);
        }

        private void ValidateInputs(int operationsPerSecond )
        {
            if(operationsPerSecond <= 0)
                throw new ArgumentOutOfRangeException("OperationsPerSecond", operationsPerSecond, "Value must be bigger than zero");
        
        }
        private int CalculateOperationSize(int operationSize)
        {
            if(operationSize < minimumSize)
                return Convert.ToInt32(minimumSize);

            int remainder = 0;
            var quotient = Math.DivRem(operationSize, minimumSize, out remainder);

            if(remainder == 0)
                return operationSize; 
         
            return minimumSize * (quotient + 1);        
        }
    }
}