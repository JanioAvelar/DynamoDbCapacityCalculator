using CapacityCalculator.API.Models;
using CapacityCalculator.Domain.Entities;
using CapacityCalculator.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CapacityCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadCapacityCalculatorController : ControllerBase
    {
        readonly IReadCapacityCalculator _rcuCalculator;
        public ReadCapacityCalculatorController(IReadCapacityCalculator rcuCalculator)
        {
            _rcuCalculator = rcuCalculator;
        }

        [HttpPost]
        [Route("/v1/CalculateReadCapacityUnits")]
        public IActionResult CalculateReadCapacityUnits(
            [FromBody] CalculateReadCapacityModel body, [FromHeader] ReadType readType
        )
        {
            if(readType != ReadType.StronglyConsistent
                && readType != ReadType.EventuallyConsistent)
                return BadRequest("Operation Type must be of type StronglyConsistent or EventuallyConsistent");

            if(body.OperationsPerSecond <= 0)
                return BadRequest("Operations per second must be bigger than zero");
            
            if(body.AverageSize <= 0)
                return BadRequest("The Average Size must be bigger than zero");
            
            var result = readType switch
            {
                ReadType.StronglyConsistent => CalculateStronglyConsistentReadCapacityUnits(body.OperationsPerSecond, body.AverageSize),
                ReadType.EventuallyConsistent => CalculateEventuallyConsistentReadCapacityUnits(body.OperationsPerSecond, body.AverageSize),
                _ => 0
            };

            if(result <= 0)
                return NotFound("System was not able to determine the RCU for your request");

            return Ok(new {RCU = result});
        }

        private int CalculateEventuallyConsistentReadCapacityUnits(
            int operationsPerSecond, 
            int averageSizeOperation)
        {
            return _rcuCalculator.CalculateStronglyConsistentCapacityUnits(
                                    operationsPerSecond, 
                                    averageSizeOperation);
        }

        private int CalculateStronglyConsistentReadCapacityUnits(
            int operationsPerSecond, 
            int averageSizeOperation)
        {
            return _rcuCalculator.CalculateEventuallyConsistentCapacityUnits(
                                    operationsPerSecond, 
                                    averageSizeOperation); 
        }



    }


}


