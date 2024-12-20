using Contracts;
using UnitsNet;

namespace Calc.Persistance
{
    public static class Extension
    {
        public static List<MathLogEntity> ToEntities(this List<MathLog> memory)
        {
            var entities = new List<MathLogEntity>();
            foreach (var mathLog in memory)
            {
                entities.Add(mathLog.ToEntity());
            }
            return entities;
        }

        public static MathLogEntity ToEntity(this MathLog mathLog)
        {
            var entity = new MathLogEntity { Math = mathLog.Math };

            if (mathLog.IQuantityResult is Length length)
            {
                entity.ResultValue = (double)length.Value;
                entity.ResultUnit = length.Unit.ToString();
            }
            else if (mathLog.IQuantityResult is Area area)
            {
                entity.ResultValue = (double)area.Value;
                entity.ResultUnit = area.Unit.ToString();
            }
            else if (mathLog.ResultDouble is double doubleValue)
            {
                entity.ResultValue = doubleValue;
                entity.ResultUnit = null;
            }
            else
            {
                throw new NotSupportedException("Unsupported result.");
            }

            return entity;
        }

        public static MathLog FromEntity(this MathLogEntity entity)
        {
            double value = entity.ResultValue;
            string unit = entity.ResultUnit;
            var mathLog = new MathLog();

            if (unit != null)
            {
                var quantity = ToIQuantity(value, unit);
                mathLog.Math = entity.Math;
                mathLog.IQuantityResult = quantity;

            }
            else
            {
                mathLog.Math = entity.Math;
                mathLog.ResultDouble = entity.ResultValue;
            }

            return mathLog;
        }

        private static IQuantity ToIQuantity(double value, string unit)
        {
            switch (unit)
            {
                case "Meter":
                    return Length.FromMeters(value);
                case "SquareMeter":
                    return Area.FromSquareMeters(value);
                case "Millimeter":
                    return Length.FromMillimeters(value);
                case "SquareMillimeter":
                    return Area.FromSquareMillimeters(value);
                default:
                    throw new NotImplementedException("Unsupported Unit");
            }
        }
    }
}
