using UnitsNet;

namespace Contracts
{
    public interface ICalculator
    {
        List<MathLog> Memory { get; set; }
        Task<double> Add(double v1, double v2);
        Task<double>Subtract(double v1, double v2);
        Task<double> Multiply(double v1, double v2);
        Task<double> Divide(double v1, double v2);
        Task<MathLog> Calculate(string input);
        Task<Mass> CalculateWeight(Volume materialVolume, Materials material);
        Task<Volume> MultiplyVolume(Length length1, Length length2, Length lenght3);
    }
}