﻿using ClassLibrary1;

namespace NewProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new RepositoryXml();
            var calculator = new Calculator(repository);
            const string filePath = @"C:\temp\Calculator.json";
            repository.LoadMemory(filePath);
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
                        repository.SaveMemory(filePath);
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

