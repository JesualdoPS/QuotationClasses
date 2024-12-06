﻿using UnitsNet;

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
            var entity = new MathLogEntity
            {
                Math = mathLog.Math,
                ResultValue = (double)mathLog.Result.Value,
                ResultUnit = mathLog.Result.Unit.ToString()
            };
            return entity;
        }

        public static MathLog FromEntity(this MathLogEntity entity)
        {
            double value = entity.ResultValue;
            string unit = entity.ResultUnit;

            var quantity = ToIQuantity(value, unit);
            var mathLog = new MathLog()
            {
                Math = entity.Math,
                Result = quantity
            };

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