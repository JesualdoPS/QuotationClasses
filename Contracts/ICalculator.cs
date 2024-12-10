using UnitsNet;

namespace Contracts
{
    public interface ICalculator
    {
        List<MathLog> Memory { get; set; }

        double Add(double v1, double v2);
        Length Add(Length v1, Length v2);
        MathLog Calculate(string input);
        Mass CalculateWeight(Volume materialVolume, Materials material);
        Area Multiply(Length length1, Length length2);
        Volume Multiply(Length length1, Length length2, Length lenght3);
    }
}