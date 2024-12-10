using Calc.BusinessLogic;
using Calc.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly Calculator _calculator;

        public CalculatorController(Calculator calculator)
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
