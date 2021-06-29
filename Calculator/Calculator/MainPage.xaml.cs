using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        double firstNum, secondNum;
        int state = 1;
        string calcOperator;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null); // make sure values are always initialized
        }

        private void OnClear(object sender, EventArgs e)
        {
            result.Text = "0";
            firstNum = 0;
            secondNum = 0;
            state = 1;
            calcOperator = "";
            // why is calcOperator declared in class rather than the event handler?
        }

        private void OnNumberClick(object sender, EventArgs e)
        {
            if(state == 3)
            {
                // clears, makes it able to add more multiple numbers
                // recall operations button grabs value on screen
                OnClear(this, null);
            }
            
            
            Button button = (Button)sender;
            // way to prevent multiple decimal points
            if (button.Text.Contains('.') && result.Text.Contains('.'))
            {
                return;
            }
            result.Text += button.Text;

            // way to prevent leading zeroes
            // unfortunately got rid of the singular 0 and 0 in front of decimals
            // add additional conditionals
            if (result.Text.Length == 2 && result.Text.StartsWith("0") && !result.Text.Contains('.'))
            {
                result.Text = result.Text.Substring(1, result.Text.Length - 1);
            }
            
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            state++;

            // IMPORTANT: crashes if equals equals in succession
            // implement feature to repeat previous operation
            if (state == 2)
            {
                // assign calcOperator in state 2, avoids setting equals as the operator
                calcOperator = button.Text;
                firstNum = Convert.ToDouble(result.Text);
                result.Text = "";
            }
            else if (state == 3)
            {
                // after hitting equals
                secondNum = Convert.ToDouble(result.Text);
                CalculateValue(firstNum, secondNum, calcOperator);
            }
        }

        private void CalculateValue(double d1, double d2, string op)
        {
            switch(op)
            {
                case "+":
                    double sum = d1 + d2;
                    result.Text = sum.ToString();
                    break;
                case "-":
                    double diff = d1 - d2;
                    result.Text = diff.ToString();
                    break;
                case "x":
                    double prod = d1 * d2;
                    result.Text = prod.ToString();
                    break;
                case "/":
                    double quot = d1 / d2;
                    result.Text = quot.ToString();
                    break;
                default:
                    result.Text = "Error";
                    break;
            }

            firstNum = 0;
            secondNum = 0;
            state = 1;
        }
    }
}
