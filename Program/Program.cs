using ClassLibrary1;
using UnitsNet;

namespace NewProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            const string filePath = @"C:\temp\Calculator.json";
            calculator.LoadMemory(filePath);

            while (true)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("1. Calculate");
                Console.WriteLine("2. Save");
                Console.WriteLine("3. Leave");

                Console.Write("Escolha uma opção: [ 1 / 2 / 3 ]");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Type a question: ");
                        string calculationInput = Console.ReadLine();
                        var result = calculator.Calculate(calculationInput);
                        Console.WriteLine(result);
                        break;
                    case "2":
                        calculator.SaveMemory(filePath);
                        Console.WriteLine("Dados salvos com sucesso!");
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}

