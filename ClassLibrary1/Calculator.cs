using System.Data;
using System.Text.RegularExpressions;
using UnitsNet;

namespace ClassLibrary1
{
    public class Calculator
    {
        private RepositoryJson _jsonRepository;

        private RepositoryXml _xmlRepository;
        
        public Calculator(RepositoryJson repository)
        {
            _jsonRepository = repository;
        }
        
        public double Add(double v1, double v2)
        {
            return v1 + v2;
        }
        public Length Add(Length v1, Length v2)
        {
            return v1 + v2;
        }

        public MathLog Calculate(string input)
        {
            var maxPosition = _jsonRepository.Memory.Count() - 1;
            var minPosition = 0;
            switch (input)
            {
                case "next":
                    if (_jsonRepository.MemoryPosition < maxPosition) _jsonRepository.MemoryPosition++;
                    break;

                case "previous":
                    if (_jsonRepository.MemoryPosition > minPosition) _jsonRepository.MemoryPosition--;
                    break;

                default:
                    var mathLog = EvaluateAndCalculate(input);
                    _jsonRepository.Memory.Add(mathLog);
                    _jsonRepository.MemoryPosition = _jsonRepository.Memory.IndexOf(mathLog);                    
                    break;
            }
            return _jsonRepository.Memory[_jsonRepository.MemoryPosition];
        }
        private MathLog EvaluateAndCalculate(string input)
        {
            MatchCollection matchingParts = Regex.Matches(input, @"(\d+)|(m{1,2})|([+*/-])");
            if (!HasRecognizedAll(input, matchingParts)) throw new FormatException("Invalid symbol");


            if (matchingParts.Count < 5)
                throw new ArgumentException("Invalid Expression");

            Length value1 = (matchingParts[1].Value == "m")
                ? Length.FromMeters(Convert.ToDouble(matchingParts[0].Value))
                : Length.FromMillimeters(Convert.ToDouble(matchingParts[0].Value));

            Length value2 = (matchingParts[4].Value == "m")
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
            var matchingTexts = matchingParts.Select(x => x.Value).ToArray();
            var inputWihoutMatches = input;
            foreach (var matchingText in matchingTexts)
            {
                inputWihoutMatches = inputWihoutMatches.Replace(matchingText, "");
            }
            inputWihoutMatches = inputWihoutMatches.Replace(" ", "");
            var hasMatchedEverything = string.IsNullOrEmpty(inputWihoutMatches);
            return hasMatchedEverything;
        }

        public Mass CalculateWeight(Volume materialVolume, Materials material)
        {
            var densities = new Dictionary<Materials, Mass>()
            {
                {Materials.Water, Mass.FromKilograms(1000)},
                {Materials.Steel, Mass.FromKilograms(7850)},
                {Materials.Aluminum, Mass.FromKilograms(2600)}
            };

            var density = densities[material];
            var mass = Mass.FromKilograms(density.Kilograms * materialVolume.CubicMeters);
            return mass;
        }

        public Area Multiply(Length length1, Length length2)
        {
            return length2 * length1;
        }
        public Volume Multiply(Length length1, Length length2, Length lenght3)
        {
            return length2 * length1 * lenght3;
        }
    }
}