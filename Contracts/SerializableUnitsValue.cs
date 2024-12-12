using UnitsNet;

namespace Contracts
{
    public class SerializableUnitsValue
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }
    public static class Extension
    {
        public static SerializableUnitsValue ToSerializable(this IQuantity request)
        {
            var entity = new SerializableUnitsValue
            {
                Value = (double)request.Value,
                Unit = request.Unit.ToString()
            };
            return entity;
        }

        public static IQuantity ToIQuantity(this SerializableUnitsValue value)
        {
            return value.Unit switch
            {
                "Meter" => Length.FromMeters(value.Value),
                "SquareMeter" => Area.FromSquareMeters(value.Value),
                "Millimeter" => Length.FromMillimeters(value.Value),
                "SquareMillimeter" => Area.FromSquareMillimeters(value.Value),
                "CubicMeter" => Volume.FromCubicMeters(value.Value),
                "CubicMillimeter" => Volume.FromCubicMillimeters(value.Value),
                _ => throw new NotImplementedException("Unsupported Unit"),
            };
        }
    }
}
