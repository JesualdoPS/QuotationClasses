using UnitsNet;

namespace Contracts
{
    public class MathLog
    {
        public string Math { get; set; }
        public IQuantity? IQuantityResult { get; set; }
        public double? ResultDouble { get; set; }
        public override string ToString()
        {
            return (IQuantityResult != null)
                ? $"{Math} = {IQuantityResult}"
                : $"{Math} = {ResultDouble}";            
        }
    }
}
