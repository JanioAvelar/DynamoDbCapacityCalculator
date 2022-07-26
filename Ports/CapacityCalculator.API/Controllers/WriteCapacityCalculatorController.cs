using CapacityCalculator.API.Models;
using CapacityCalculator.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CapacityCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WriteCapacityCalculatorController : ControllerBase
    {
        readonly IWriteCapacityCalculator _wcuCalculator;
        public WriteCapacityCalculatorController(IWriteCapacityCalculator wcuCalculator)
        {
            _wcuCalculator = wcuCalculator;
        }

        [HttpPost]
        [Route("/v1/CalculateWriteCapacityUnits")]
        public IActionResult CalculateWriteCapacityUnits(
            [FromBody] CalculateWriteCapacityModel body
        )
        {
            if(body.OperationsPerSecond <= 0)
                return BadRequest("Operations per second must be bigger than zero");
            
            if(body.AverageSize <= 0)
                return BadRequest("The Average Size must be bigger than zero");
            
            var result = _wcuCalculator.CalculateCapacityUnits(body.OperationsPerSecond, body.AverageSize);
            
            if(result <= 0)
                return NotFound("System was not able to determine the WCU for your request");

            return Ok(new {WCU = result});
        }

    }


}


