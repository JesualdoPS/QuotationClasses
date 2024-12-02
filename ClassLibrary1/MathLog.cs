using System.Text.Json.Serialization;
using UnitsNet;

namespace ClassLibrary1
{
    public class MathLog
    {
        public string Math { get; set; }
        [JsonConverter(typeof(QuantityJsonConverter))]
        public IQuantity Result { get; set; }
    }
}
