using Calc.Persistance;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWeb.Controllers
{
    [ApiController]
    [Route("Calculator")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator _calculator;

        public CalculatorController(ICalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("Calculate")]
        public MathLogEntity Calculate(CalculateRequest request)
        {
            var mathLog = _calculator.Calculate(request.Input);
            var mathLogEntity = mathLog.ToEntity();
            return mathLogEntity;
        }

        public class CalculateRequest
        {
            public string Input { get; set; }
        }
    }
}
