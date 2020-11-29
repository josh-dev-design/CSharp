using System.Linq;
using System.Windows.Forms;
using System;

namespace BasicCalculator
{
    #region Constructor
    /// <summary>
    /// Default constructor
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Calculation_Click(object sender, EventArgs e)
        { 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertValueText(".");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertValueText("0");
        }
        private void button16_Click(object sender, EventArgs e)
        {
            InsertValueText("*");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            deleteTextValue();
        }

        private void deleteTextValue()
        {
            if (this.UserInputText.Text.Length < this.UserInputText.SelectionStart + 1)
                return;
            var selectionStart = this.UserInputText.SelectionStart;
            this.UserInputText.Text = this.UserInputText.Text.Remove(this.UserInputText.SelectionStart, 1);
            this.UserInputText.SelectionStart = selectionStart;
            this.UserInputText.SelectionLength = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            calculateEquation();
        }
        private void calculateEquation()
        {

            this.Calculation.Text = ParseOperation();
        }

        private string ParseOperation()
        {
            try
            {
                var userInput = this.UserInputText.Text;

                userInput = userInput.Replace(" ", "");

                var operation = new Operation();
                var leftside = true;

                for (int i = 0; i < userInput.Length; i++)
                {

                    if ("01234567890.".Any(c => userInput[i] == c))
                    {
                        if (leftside)
                            operation.leftSide = AddNumberPart(operation.leftSide, userInput[i]);
                        else
                            operation.rightSide = AddNumberPart(operation.rightSide, userInput[i]);
                    }

                    else if ("-+*/.".Any(c => userInput[i] == c))
                    {
                        if (!leftside)
                        {
                            var operatorType = GetOperationType(userInput[i]);
                            if (operation.rightSide.Length == 0)
                            {
                                if (operatorType != typeof(OperationType))
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an right side number");

                                operation.rightSide += userInput[i];
                            }
                            else
                            {
                                operation.leftSide = CalculateOperation(operation);
                                operation.OperationType = (OperationType)operatorType;
                                operation.rightSide = string.Empty;
                            }
                        }
                        else
                        {
                            var operatorType = GetOperationType(userInput[i]);
                            if (operation.leftSide.Length == 0)
                            {
                                if (operatorType != typeof(OperationType))
                                {
                                   
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an right side number");

                                    operation.leftSide += userInput[i];
                                }
                                else
                                {
                                    operation.OperationType = (OperationType)operatorType;
                                    leftside = false;
                                }

                            }
                            
                        }
                    }
                   
                }

                return CalculateOperation(operation);
          
            }
            catch (Exception ex)
            {
                return $"Invalid Equation. { ex.Message }";
            }
        }
      
        private string CalculateOperation(Operation operation)
        {
            decimal left = 0;
            decimal right = 0;
            if (string.IsNullOrEmpty(operation.leftSide) || !decimal.TryParse(operation.leftSide, out left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.leftSide}");

            try
            {
                switch (operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();

                    case OperationType.Minus:
                        return (left - right).ToString();
                    case OperationType.Multiply:
                        return (left * right).ToString();
                    case OperationType.Divide:
                        return (left / right).ToString();
                    default:
                        throw new InvalidOperationException($"Unknown Operator while calculating operation. { operation.OperationType }");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{operation.leftSide} {operation.OperationType} {operation.rightSide} {ex.Message}");
            }
        }
        private object GetOperationType(char character)
        {
            switch (character)
            {
                case '+':
                    return OperationType.Add;
                    ;
                case '-':
                    return OperationType.Minus;
                case '*':
                    return OperationType.Multiply;
                case '/':
                    return OperationType.Divide;
                default:
                    throw new InvalidOperationException($"Unknown Operator type {character}");
            }
        }

        private string AddNumberPart(string currentCharacter, char newCharacter)
        {
            if (newCharacter == '.' && currentCharacter.Contains('.'))
                throw new InvalidOperationException($"Number {currentCharacter} already containe a . and another cannot be added");

            return currentCharacter + newCharacter;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.UserInputText.Text = string.Empty;
            UserInputText.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            InsertValueText("/");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            InsertValueText("7");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            InsertValueText("8");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            InsertValueText("9");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InsertValueText("4");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            InsertValueText("5");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            InsertValueText("6");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            InsertValueText("-");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertValueText("1");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertValueText("2");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertValueText("3");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InsertValueText("+");
        }
        private void InsertValueText(string value)
        {
            var selectionStart = this.UserInputText.SelectionStart;
            this.UserInputText.Text = this.UserInputText.Text.Insert(this.UserInputText.SelectionStart, value);
            this.UserInputText.SelectionStart = selectionStart + value.Length;
            this.UserInputText.SelectionLength = 0;
        }

        private void Calculation_Click_1(object sender, EventArgs e)
        {

        }
    }
}