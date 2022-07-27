using System;
using CapacityCalculator.Application.Services;
using Xunit;

namespace CapacityCalculator.Tests.Application;

public class RCUCalculatorTests
{
    private RCUCalculator rCUCalculator = new RCUCalculator();

    [Theory]
    [InlineData(10,4,10)]
    [InlineData(10,8,20)]
    [InlineData(5,4,5)]
    [InlineData(10,2,10)]
    [InlineData(10,6,20)]
    public void GivenTwoPositiveNumbersTheResultOfStronglyConsistentReadMustBeCalculatedCorrectly(
        int operationsPerSecond,
        int averageSize,
        int expectedResult
    )
    {
        // When
        var result = rCUCalculator.CalculateStronglyConsistentCapacityUnits(operationsPerSecond,averageSize);
        
        // Then
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void WhenCalculateSCWithOperationSizeEqualZeroShouldGenerateException()
    {
        // Given
        var operationsPerSecond = 0;
        var averageSize = 4;

        var expectedException = new ArgumentOutOfRangeException("OperationsPerSecond", operationsPerSecond, "Value must be bigger than zero");
        
        // When
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => rCUCalculator.CalculateStronglyConsistentCapacityUnits(operationsPerSecond,averageSize));

        // Then
        Assert.Equal(expectedException.Message, ex.Message);
        Assert.Equal(expectedException.GetType() ,ex.GetType());
    }

    
    [Fact]
    public void WhenCalculateSCWithAverageSizeEqualZeroShouldCalculateAverageSizeAsMinimumSize()
    {
        // Given
        var operationsPerSecond = 10;
        var averageSize = 0;

        var expectedResult = 10;

        // When
        var result = rCUCalculator.CalculateStronglyConsistentCapacityUnits(operationsPerSecond,averageSize);

        // Then
        Assert.Equal(expectedResult, result);
    }
}
