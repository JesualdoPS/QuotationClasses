using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace ClassLibrary1
{
    public class QuantityJsonConverter : JsonConverter<IQuantity>
    {
        public override IQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            double value = 0;
            string unit = "";

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "Value":
                            value = reader.GetDouble();
                            break;
                        case "Unit":
                            unit = reader.GetString();
                            break;
                    }
                }
            }
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
                    throw new JsonException("Unsupported Unit");
            }

        }

        public override void Write(Utf8JsonWriter writer, IQuantity value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("Value", (double)value.Value);
            writer.WriteString("Unit", value.Unit.ToString());
            writer.WriteEndObject();
        }
    }
}
