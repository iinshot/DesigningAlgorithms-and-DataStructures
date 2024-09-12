using System.Net.Http.Headers;
using System.Transactions;

public struct Complex
{
    public double Re;
    public double Im;
    public Complex(double re, double im)
    {
        Re = re;
        Im = im;
    }
   
    // sum between 2 complex numbers
    public static Complex operator + (Complex x, Complex y)
    {
        return new (x.Re + y.Re, x.Im + y.Im);
    }

    // difference between 2 complex numbers
    public static Complex operator - (Complex x, Complex y)
    {
        return new (x.Re - y.Re, x.Im - y.Im);
    }

    // product between 2 complex numbers
    public static Complex operator * (Complex x, Complex y)
    {
        return new ((x.Re * y.Re) - (x.Im * y.Im), (x.Re * y.Im) + (y.Re * x.Im));
    }

    // division between 2 complex numbers
    public static Complex operator / (Complex x, Complex y)
    {
        double denominator = (y.Re * y.Re) + (y.Im * y.Im);
        return new(((x.Re * y.Re) + (x.Im * y.Im)) / denominator, ((x.Im * y.Re) - (x.Re * y.Im)) / denominator);
    }

    // module of complex number
    public static double ComplexModule(Complex x)
    {
        return Math.Sqrt(x.Re * x.Re + x.Im * x.Im);
    }
    
    // argument of complex number
    public static double ComplexArgument(Complex x)
    {
        return Math.Atan(x.Im / x.Re);
    }

    // print complex numbers
    public static void Print(Complex x)
    {
        if (x.Im >= 0) Console.WriteLine($"{x.Re} + {x.Im}i");
        else Console.WriteLine($"{x.Re} - {Math.Abs(x.Im)}i");
    }
    public static void PrintRe(Complex x)
    {
        Console.WriteLine(x.Re);
    }
    public static void PrintIm(Complex x)
    {
        Console.WriteLine($"{x.Im}i");
    }
}

class Programm
{
    static void Main()
    {
        bool flag = true;
        Complex number1 = new Complex(0, 0);
        Complex number2 = new Complex(0, 0);
        while (flag)
        {
            Console.WriteLine("Enter 0 for create complex number.");
            Console.WriteLine("Enter 1 for sum complex numbers.");
            Console.WriteLine("Enter 2 for difference complex numbers.");
            Console.WriteLine("Enter 3 for product complex numbers.");
            Console.WriteLine("Enter 4 for division complex numbers.");
            Console.WriteLine("Enter 5 for module complex number.");
            Console.WriteLine("Enter 6 for argument complex number.");
            Console.WriteLine("Enter 7 for output complex number.");
            Console.WriteLine("Enter 8 for output real part of complex number.");
            Console.WriteLine("Enter 9 for output imaginary part of complex number.");
            Console.WriteLine("Enter Q or q for exit.");
            char enter;
            enter = Convert.ToChar(Console.ReadLine());
            switch (enter)
            {
                case '0':
                    Console.WriteLine("Enter a real part of complex number.");
                    double re1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of complex number.");
                    double im1 = Convert.ToDouble(Console.ReadLine());
                    number1 = new Complex(re1, im1);
                    continue;
                case '1':
                    Console.WriteLine("Enter a real part of second complex number.");
                    double re2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    double im2 = Convert.ToDouble(Console.ReadLine());
                    number2 = new Complex(re2, im2);
                    Complex sum = number1 + number2;
                    Complex.Print(sum);
                    continue;
                case '2':
                    Console.WriteLine("Enter a real part of second complex number.");
                    re2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    im2 = Convert.ToDouble(Console.ReadLine());
                    number2 = new Complex(re2, im2);
                    Complex difference = number1 - number2;
                    Complex.Print(difference);
                    continue;
                case '3':
                    Console.WriteLine("Enter a real part of second complex number.");
                    re2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    im2 = Convert.ToDouble(Console.ReadLine());
                    number2 = new Complex(re2, im2);
                    Complex product = number1 * number2;
                    Complex.Print(product);
                    continue;
                case '4':
                    Console.WriteLine("Enter a real part of second complex number.");
                    re2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    im2 = Convert.ToDouble(Console.ReadLine());
                    number2 = new Complex(re2, im2);
                    Complex division = number1 / number2;
                    Complex.Print(division);
                    continue;
                case '5':
                    double absolut = Complex.ComplexModule(number1);
                    Console.WriteLine(absolut);
                    continue;
                case '6':
                    double argument = Complex.ComplexArgument(number1);
                    Console.WriteLine(argument);
                    continue;
                case '7':
                    Complex.PrintRe(number1); 
                    Console.Write("+");
                    Complex.PrintIm(number2);
                    continue;
                case '8':
                    Complex.PrintRe(number1);
                    continue;
                case '9':
                    Complex.PrintIm(number2);
                    continue;
                case 'q':
                    flag = false;
                    break;
                case 'Q':
                    flag = false;
                    break;
                default:
                    Console.WriteLine("Unknown Error.");
                    continue;
            }
        }
    }
}
