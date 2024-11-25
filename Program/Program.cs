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
            Console.WriteLine("Options:");
            Console.WriteLine("1. Calculate");
            Console.WriteLine("2. Save");
            Console.WriteLine("3. Leave");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Choose an option [ 1 / 2 / 3 ]: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Type a question: ");
                        string calculation = Console.ReadLine();
                        var result = calculator.Calculate(calculation);
                        Console.WriteLine(result.Result);
                        break;

                    case "2":
                        calculator.SaveMemory(filePath);
                        Console.WriteLine("Data saved!");
                        break;

                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}

