using Calc.Persistance;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using UnitsNet;

namespace CalculatorWeb.Controllers
{
    [ApiController]
    [Route("Calculator")]
    public partial class CalculatorController : ControllerBase
    {
        private readonly ICalculator _calculator;      

        public CalculatorController(ICalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("Calculate")]
        public async Task<MathLogEntity> Calculate(CalculateRequest request)
        {
            var mathLog = await _calculator.Calculate(request.Input);
            var mathLogEntity = mathLog.ToEntity();
            return mathLogEntity;
        }

        [HttpPost("AddNumbers")]
        public async Task<double> Add(NormalRequest request)
        {
            var result = await _calculator.Add(request.Value1, request.Value2);
            return result;
        }

        [HttpPost("SubtractNumbers")]
        public async Task<double> Subtract(NormalRequest request)
        {
            var result = await _calculator.Subtract(request.Value1, request.Value2);
            return result;
        }

        [HttpPost("MultiplyNumbers")]
        public async Task<double> Multiply(NormalRequest request)
        {
            var result = await _calculator.Multiply(request.Value1, request.Value2);
            return result;
        }

        [HttpPost("DivideNumbers")]
        public async Task<double> Divide(NormalRequest request)
        {
            var result = await _calculator.Divide(request.Value1, request.Value2);
            return result;
        }

        [HttpPost("MultiplyVolume")]
        public async Task<SerializableUnitsValue> MultiplyVolume(VolumeRequest request)
        {
            var value1 = (Length)request.Length1.ToIQuantity();
            var value2 = (Length)request.Length2.ToIQuantity();
            var value3 = (Length)request.Length3.ToIQuantity();
            var result = await _calculator.MultiplyVolume(value1, value2, value3);
            return result.ToSerializable();
        }     
    }
}
