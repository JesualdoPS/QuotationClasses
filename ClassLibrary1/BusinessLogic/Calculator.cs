using System.Data;
using System.Text.RegularExpressions;
using Contracts;
using UnitsNet;

namespace Calc.BusinessLogic
{
    public class Calculator : ICalculator
    {
        private IRepository _repository;
        public List<MathLog> Memory { get; set; } = new List<MathLog>();

        private Dictionary<Materials, Mass> _densities = new Dictionary<Materials, Mass>()
            {
                {Materials.Water, Mass.FromKilograms(1000)},
                {Materials.Steel, Mass.FromKilograms(7850)},
                {Materials.Aluminum, Mass.FromKilograms(2600)}
            };

        public Calculator(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<double> Add(double v1, double v2)
        {
            return v1 + v2;
        }
        public async Task<double> Subtract(double v1, double v2)
        {
            return v1 - v2;
        }
        public async Task<double> Multiply(double v1, double v2)
        {
            return v1 * v2;
        }
        public async Task<double> Divide(double v1, double v2)
        {
            return v1 / v2;
        }

        public async Task<MathLog> Calculate(string input)
        {
            var maxPosition = _repository.Memory.Count() - 1;
            var minPosition = 0;
            switch (input)
            {
                case "next":
                    if (_repository.MemoryPosition < maxPosition) _repository.MemoryPosition++;
                    break;

                case "previous":
                    if (_repository.MemoryPosition > minPosition) _repository.MemoryPosition--;
                    break;

                default:
                    var mathLog = EvaluateAndCalculate(input);                    
                    _repository.Memory.Add(mathLog);
                    Memory.Add(mathLog);
                    _repository.MemoryPosition = _repository.Memory.IndexOf(mathLog);
                    break;
            }
            var result = _repository.Memory[_repository.MemoryPosition];
            return await Task.FromResult(result);
        }
        private MathLog EvaluateAndCalculate(string input)
        {
            MatchCollection matchingParts = Regex.Matches(input, @"(\d+)|(m{1,2})|([+*/-])");
            if (!HasRecognizedAll(input, matchingParts)) throw new FormatException("Invalid symbol");

            if (matchingParts.Count < 5)
                throw new ArgumentException("Invalid Expression");

            Length value1 = matchingParts[1].Value == "m"
                ? Length.FromMeters(Convert.ToDouble(matchingParts[0].Value))
                : Length.FromMillimeters(Convert.ToDouble(matchingParts[0].Value));

            Length value2 = matchingParts[4].Value == "m"
                ? Length.FromMeters(Convert.ToDouble(matchingParts[3].Value))
                : Length.FromMillimeters(Convert.ToDouble(matchingParts[3].Value));

            var operatorSymbol = matchingParts[2].Value;

            IQuantity result = operatorSymbol switch
            {
                "+" => value1 + value2,
                "-" => value1 - value2,
                "*" => value1 * value2,
            };
            var mathLog = new MathLog { Math = input, Result = result };
            return mathLog;
        }

        private static bool HasRecognizedAll(string input, MatchCollection matchingParts)
        {
            var matchingTexts = matchingParts
                .Select(x => x.Value)
                .OrderByDescending(x => x.Length)
                .ToArray();
            
            var inputWihoutMatches = input;
            foreach (var matchingText in matchingTexts)
            {
                inputWihoutMatches = inputWihoutMatches.Replace(matchingText, "");
            }
            inputWihoutMatches = inputWihoutMatches.Replace(" ", "");
            var hasMatchedEverything = string.IsNullOrEmpty(inputWihoutMatches);
            return hasMatchedEverything;
        }

        public async Task<Mass> CalculateWeight(Volume materialVolume, Materials material)
        {
            var density = _densities[material];
            var mass = Mass.FromKilograms(density.Kilograms * materialVolume.CubicMeters);
            return mass;
        }

        public async Task<Volume> MultiplyVolume(Length length1, Length length2, Length lenght3)
        {
            return length1 * length2 * lenght3;
        }
    }
}