using System.Text;
using Calc.Persistance;
using Newtonsoft.Json;


namespace CalculatorApp
{
    public partial class CalculatorApp : Form
    {
        public CalculatorApp()
        {
            InitializeComponent();            
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

        private async void btnEqualTo_Click(object sender, EventArgs e)
        {
            var input = screen.Text;
            var api = new WebApiClient();
            var mathLog = await api.Calculate(input);
            screen.Text = mathLog.Result.ToString();
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

        private void Previous_Click(object sender, EventArgs e)
        {
            //screen.Text = _calculator.Calculate("previous").ToString();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            //screen.Text = _calculator.Calculate("next").ToString();
        }
    }
}
