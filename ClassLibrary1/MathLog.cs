using UnitsNet;

namespace ClassLibrary1
{
    public class MathLog
    {
        public string Math { get; set; }
        public IQuantity Result { get; set; }

        public string ResultToString()
        {
            return Result.ToString();
        }

        public static MathLog FromString(string math, string result)
        {
            var parts = result.Split(' ');
            var value = double.Parse(parts[0]);
            var unit = parts[1];

            IQuantity quantity = unit switch
            {
                "m" => Length.FromMeters(value),
                "mm" => Length.FromMillimeters(value),
                "m²" => Area.FromSquareMeters(value),
                "mm²" => Area.FromSquareMillimeters(value),
                _ => throw new ArgumentException("Unsupported unit")
            };

            return new MathLog { Math = math, Result = quantity };
        }
    }
    public class MathLogEntity
    {
        public string Math { get; set; }
        public string Result { get; set; }
    }
}
