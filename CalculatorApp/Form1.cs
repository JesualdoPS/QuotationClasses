using System.Text.RegularExpressions;
using Azure.Core;
using Contracts;
using UnitsNet;


namespace CalculatorApp
{
    public partial class CalculatorApp : Form
    {
        private readonly ICalculator _calculator;

        public CalculatorApp()
        {
            InitializeComponent();
            _calculator = new Calculator();
        }

        private void screen_TextChanged(object sender, EventArgs e) { }

        private void btn1_Click(object sender, EventArgs e)
        {
            screen.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            screen.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            screen.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            screen.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            screen.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            screen.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            screen.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            screen.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            screen.Text += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            screen.Text += "0";
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            screen.Text += ",";
        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            screen.Text += " ";
        }

        private void btnMeter_Click(object sender, EventArgs e)
        {
            screen.Text += "m";
        }

        private void btnMillimeter_Click(object sender, EventArgs e)
        {
            screen.Text += "mm";
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            screen.Text += " + ";
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            screen.Text += " - ";
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            screen.Text += " * ";
        }
        private void btnDivide_Click(object sender, EventArgs e)
        {
            screen.Text += " / ";
        }
        private async void btnEqualTo_Click(object sender, EventArgs e)
        {
            if (screen.Text.Contains("m") || screen.Text.Contains("mm"))
            {
                if (Regex.Matches(screen.Text, "m|mm").Count == 2)
                {
                    var mathLog = await _calculator.Calculate(screen.Text);
                    screen.Text = mathLog.Result.ToString();
                }
                else if (Regex.Matches(screen.Text, "m|mm").Count > 2)
                {
                    MatchCollection matchingParts = Regex.Matches(screen.Text, @"(\d+)|(m{1,2})|([+*/-])");

                    Length length1 = matchingParts[1].Value == "m"
                ? Length.FromMeters(Convert.ToDouble(matchingParts[0].Value))
                : Length.FromMillimeters(Convert.ToDouble(matchingParts[0].Value));

                    Length length2 = matchingParts[4].Value == "m"
                ? Length.FromMeters(Convert.ToDouble(matchingParts[3].Value))
                : Length.FromMillimeters(Convert.ToDouble(matchingParts[3].Value));

                    Length length3 = matchingParts[7].Value == "m"
                ? Length.FromMeters(Convert.ToDouble(matchingParts[6].Value))
                : Length.FromMillimeters(Convert.ToDouble(matchingParts[6].Value));

                    var result = await _calculator.MultiplyVolume(length1, length2, length3);
                    screen.Text = result.ToString();
                }

                else
                {
                    throw new FormatException("Invalid Expression");
                }
            }
            else
            {
                MatchCollection matchingParts = Regex.Matches(screen.Text, @"(\d+)|([+*/-])");
                double value1 = Convert.ToDouble(matchingParts[0].Value);
                double value2 = Convert.ToDouble(matchingParts[2].Value);
                var operatorSymbol = matchingParts[1].Value;
                double result = operatorSymbol switch
                {
                    "+" => await _calculator.Add(value1, value2),
                    "-" => await _calculator.Subtract(value1, value2),
                    "*" => await _calculator.Multiply(value1, value2),
                    "/" => await _calculator.Add(value1, value2)
                };
                screen.Text = result.ToString();
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            screen.Text = screen.Text.Substring(0, screen.Text.Length - 1);
            screen.SelectionStart = screen.Text.Length;
        }

        private void btnClearEverything_Click(object sender, EventArgs e)
        {
            screen.Text = "";
        }

        private void saveSQL_Click(object sender, EventArgs e)
        {
            //string filePath = "";
            //_repository.SaveMemory(filePath);
            //screen.Text = "Data Saved on dbCalculator";
        }

        private void saveXML_Click(object sender, EventArgs e)
        {
            //string filePath =
            //    @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Storage\Calculator.xml";
            //_repository = new RepositoryXml(_calculator.Memory);
            //_repository.SaveMemory(filePath);
        }

        private void saveJson_Click(object sender, EventArgs e)
        {
            //string filePath =
            //    @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Storage\Calculator.json";
            //_repository = new RepositoryJson(_calculator.Memory);
            //_repository.SaveMemory(filePath);
        }

        private async void Previous_Click(object sender, EventArgs e)
        {
            var previous = await _calculator.Calculate("previous");
            screen.Text = previous.ToString();
        }

        private async void Next_Click(object sender, EventArgs e)
        {
            var previous = await _calculator.Calculate("next");
            screen.Text = previous.ToString();
        }
    }
}
