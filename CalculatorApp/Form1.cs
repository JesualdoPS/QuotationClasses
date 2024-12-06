using System.Data;
using System.DirectoryServices.ActiveDirectory;
using Calc.BusinessLogic;
using Calc.Persistance;

namespace CalculatorApp
{
    public partial class CalculatorApp : Form
    {
        private IRepository _repository;
        private Calculator _calculator;

        public CalculatorApp()
        {
            InitializeComponent();

            _repository = new RepositorySQL();
            _calculator = new Calculator(_repository);
        }       

        private void screen_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

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

        private void btnEqualTo_Click(object sender, EventArgs e)
        {            
            var result = _calculator.Calculate(screen.Text);
            screen.Text = Convert.ToString(result.Result);
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClearEverything_Click(object sender, EventArgs e)
        {
            screen.Text = "";
        }

        private void saveSQL_Click(object sender, EventArgs e)
        {

        }

        private void saveXML_Click(object sender, EventArgs e)
        {

        }

        private void saveJson_Click(object sender, EventArgs e)
        {

        }

        private void Previous_Click(object sender, EventArgs e)
        {

        }

        private void Next_Click(object sender, EventArgs e)
        {

        }
    }
}
