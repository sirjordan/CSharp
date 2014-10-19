    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
     
    public class ExtendedExpressionEvaluator
    {
        private static decimal[] ExtractNumbers(string expression)
        {
            bool hasMinusInExpretion = false;
            if (expression.Contains("*-") || expression.Contains("/-"))
            {
                int index = expression.IndexOf("*-") + 1;
                expression = expression.Remove(index, 1);
                hasMinusInExpretion = true;
            }
            if (expression[0] == '-')
            {
                expression = expression.Remove(0, 1);
                hasMinusInExpretion = true;
            }
     
            string[] splitResult = expression.Split('+', '-');
            List<decimal> numbers = new List<decimal>();
     
            foreach (string number in splitResult)
            {
                if (number.Contains("*"))
                {
                    string[] multiplyParts = number.Split('*');
                    numbers.Add(decimal.Parse(multiplyParts[0]) * decimal.Parse(multiplyParts[1]));
                }
                else if (number.Contains("/"))
                {
                    string[] multiplyParts = number.Split('/');
                    numbers.Add(decimal.Parse(multiplyParts[0]) / decimal.Parse(multiplyParts[1]));
                }
                else
                {
                    numbers.Add(decimal.Parse(number));
                }
            }
     
            if (hasMinusInExpretion)
            {
                numbers[0] = numbers[0] * -1;
            }
            return numbers.ToArray();
        }
     
        private static char[] ExtractOperators(string expression)
        {
            string operationsCharacters = "+-";
            List<char> operators = new List<char>();
            foreach (char c in expression)
            {
                if (operationsCharacters.Contains(c))
                {
                    operators.Add(c);
                }
            }
     
            return operators.ToArray();
        }
     
        private static string ProcceedBraces(string expretion)
        {
            StringBuilder resultWithoutBraces = new StringBuilder(expretion);
            while (resultWithoutBraces.ToString().Contains('('))
            {
                for (int i = 0; i < resultWithoutBraces.Length; i++)
                {
                    if (resultWithoutBraces[i] == ')')
                    {
                        int indexOfLastBrace = i;
                        while (resultWithoutBraces[indexOfLastBrace] != '(')
                        {
                            indexOfLastBrace--;
                        }
     
                        StringBuilder expretionInBraces = new StringBuilder();
                        for (int j = indexOfLastBrace + 1; ; j++)
                        {
                            if (resultWithoutBraces[j] == ')')
                            {
                                break;
                            }
                            else
                            {
                                expretionInBraces.Append(resultWithoutBraces[j]);
                            }
                        }
     
                        decimal[] numbers = ExtractNumbers(expretionInBraces.ToString());
                        char[] operators = ExtractOperators(expretionInBraces.ToString());
                        decimal resultInsideBraces = CalculateExpression(numbers, operators);
                        resultWithoutBraces.Replace("(" + expretionInBraces.ToString() + ")", resultInsideBraces.ToString());
                        break;
                    }
                }
            }
     
            return resultWithoutBraces.ToString();
        }
     
        private static decimal CalculateExpression(decimal[] numbers, char[] operators)
        {
            decimal result = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                char operation = operators[i - 1];
                decimal nextNumber = numbers[i];
     
                if (operation == '+')
                {
                    result += nextNumber;
                }
                else if (operation == '-')
                {
                    result -= nextNumber;
                }
            }
     
            return result;
        }
     
        private static string ReadExpression()
        {
            Console.WriteLine("Enter expression:");
            string expression = Console.ReadLine();
            return expression;
        }
     
        static void Main()
        {
            try
            {
                string expression = ReadExpression();
                expression = ProcceedBraces(expression);
                decimal[] numbers = ExtractNumbers(expression);
                char[] operators = ExtractOperators(expression);
                decimal result = CalculateExpression(numbers, operators);
                Console.WriteLine("Result: {0}", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid expression!");
                Console.WriteLine(ex.Message);
            }
        }
    }

