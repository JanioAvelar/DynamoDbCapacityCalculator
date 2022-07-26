using CapacityCalculator.Application.Services;

Console.WriteLine("WCU/RCU Calculator");

var rcuCalculator = new RCUCalculator();
var wcuCalculator = new WCUCalculator();

var rcuSCC = rcuCalculator.CalculateStronglyConsistentCapacityUnits(10, 13);

Console.WriteLine(rcuSCC);





