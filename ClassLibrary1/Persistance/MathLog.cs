using UnitsNet;

namespace Calc.Persistance
{
    public class MathLog
    {
        public string Math { get; set; }
        public IQuantity Result { get; set; }
    }
}
