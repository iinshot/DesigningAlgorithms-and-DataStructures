using MyStackLib;
using System.IO;
using System.Runtime.CompilerServices;
class ReversePollandNotation
{
    // priority of operator
    static public int Priority(string path)
    {
        switch (path)
        {
            case "+": return 1;
            case "-": return 1;
            case "*":
            case "/":
            case "//":
                return 2;
            case "^": return 3;
            case "sqrt":
            case "abs":
            case "sin":
            case "cos":
            case "tg":
            case "ln":
            case "log":
            case "min":
            case "max":
            case "mod":
            case "exp":
                return 4;
            case "%": return 5;
            default: return 0;
        }
    }

    // description of operation
    private static double DescriptionOperation(string path, params double[] array) => path switch
    {
        "+" => array[0] + array[1],
        "-" => array[0] - array[1],
        "*" => array[0] * array[1],
        "/" => array[0] / array[1],
        "^" => Math.Pow(array[0], array[1]),
        "sqrt" => Math.Sqrt(array[0]),
        "abs" => Math.Abs(array[0]),
        "//" => Math.Floor(array[0] / array[1]),
        "exp" => Math.Exp(array[0]),
        "sin" => Math.Sin(array[0]),
        "cos" => Math.Cos(array[0]),
        "tg" => Math.Tan(array[0]),
        "ln" => Math.Log(array[0]),
        "log" => Math.Log10(array[0]),
        "min" => array[0] < array[1] ? array[0] : array[1],
        "max" => array[0] > array[1] ? array[0] : array[1],
        "mod" => (int)array[0] % (int)array[1],
        _ => 0
    };

    // for reading full label
    private static string GetLabel(ref int position, string expression)
    {
        string output = "";
        for (; position < expression.Length; position++)
        {
            if (Char.IsLetter(expression[position]) || expression[position] == '/')
                output += expression[position];
            else
            {
                position--;
                break;
            }
        }
        return output;
    }

    // for reading full number 
    private static string GetNumber(ref int position, string expression)
    {
        string output = "";
        for (; position < expression.Length; position++)
        {
            if (Char.IsDigit(expression[position]))
                output += expression[position];
            else
            {
                position--;
                break;
            }
        }
        return output;
    }

    // transformation to reverse polland notation
    public static string Transform(string expression)
    {
        string after = "";
        MyStack<string> stack = new MyStack<string>();
        for (int i = 0; i < expression.Length; i++)
        {
            if (Char.IsDigit(expression[i]))
                after += GetNumber(ref i, expression) + " ";
            else if (Char.IsLetter(expression[i]) || (expression[i] == '/' && expression[i + 1] == '/'))
            {
                string path = "";
                path += GetLabel(ref i, expression);
                if (Priority(path) != 0)
                {
                    while (!stack.Empty() && (Priority(stack.Peek()) >= Priority(path)))
                    {
                        after += stack.Peek();
                        stack.Pop();
                    }
                    stack.Push(path);
                }
            }
            else if (Priority(Convert.ToString(expression[i])) != 0)
            {
                while (!stack.Empty() && (Priority(stack.Peek()) >= Priority(Convert.ToString(expression[i])))){
                    after += stack.Peek() + " ";
                    stack.Pop();
                }
                stack.Push(Convert.ToString(expression[i]));
            }
            else if (expression[i] == '(' && i + 1 != expression.Length && expression[i + 1] == '-')
            {
                if (Char.IsDigit(expression[i + 2]))
                {
                    i += 2;
                    string number = GetNumber(ref i, expression);
                    after += Convert.ToString('~') + number + " ";
                    i += 1;
                }
            }
            else if (expression[i] == '(')
                stack.Push(Convert.ToString(expression[i]));
            else if (expression[i] == ')')
            {
                while (stack.Peek() != "(")
                {
                    after += stack.Peek() + " ";
                    stack.Pop();
                }
                stack.Pop();
            }
        }
        while (!stack.Empty())
        {
            after += stack.Peek() + " ";
            stack.Pop();
        }
        return after;
    }

    // for calculating
    public static double Calculate(string expression)
    {
        string name = "";
        MyStack<double> node = new MyStack<double>();
        for (int i = 0; i < expression.Length; i++)
        {
            name = "";
            if (Char.IsDigit(expression[i]))
            {
                string number = GetNumber(ref i, expression);
                node.Push(Convert.ToDouble(number));
            }
            else if (Char.IsLetter(expression[i]) || (expression[i] == '/' && expression[i + 1] == '/'))
                name += GetLabel(ref i, expression);
            else if (expression[i] == '~' && (i + 1 < expression.Length || i + 2 < expression.Length))
            {
                if (Char.IsDigit(expression[i + 1]))
                {
                    i += 1;
                    string number = GetNumber(ref i, expression);
                    double number2 = Convert.ToDouble(number);
                    node.Push(-number2);
                    i += 1;
                }
            }
            else if (Priority(Convert.ToString(expression[i])) != 0)
            {
                double number1 = node.Peek();
                node.Pop();
                double number2 = node.Peek();
                node.Pop();
                node.Push(DescriptionOperation(Convert.ToString(expression[i]), number2, number1));
            }
            if (Priority(name) != 0 && name == "//")
            {
                double number1 = node.Peek();
                node.Pop();
                double number2 = node.Peek();
                node.Pop();
                node.Push(DescriptionOperation(name, number2, number1));
            }
            if (Priority(name) != 0)
            {
                if (Priority(name) == 4 && (name == "mod" || name == "max" || name == "min"))
                {
                    double number1 = node.Peek();
                    node.Pop();
                    double number2 = node.Peek();
                    node.Pop();
                    node.Push(DescriptionOperation(Convert.ToString(name), number2, number1));
                }
                else if(Priority(name) == 4 && (name != "mod" || name != "max" || name != "min"))
                {
                    double number = node.Peek();
                    node.Pop();
                    node.Push(DescriptionOperation(Convert.ToString(name), number));
                }
            }
        }
        return node.Peek();
    }
    
    static void Main(string[] args)
    {
        string arguments = "(7*2)^5";
        string path = Transform(arguments);
        Console.WriteLine(path);
        double result = Calculate(path);
        Console.WriteLine(result);
    }
}