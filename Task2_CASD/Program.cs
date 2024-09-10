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
    public static void Print_Re(Complex x)
    {
        Console.WriteLine(x.Re);
    }
    public static void Print_Im(Complex x)
    {
        Console.WriteLine($"{x.Im}i");
    }
}

class Programm
{
    static void Main()
    {
        bool flag = true;
        Complex number_1 = new Complex(0, 0);
        Complex number_2 = new Complex(0, 0);
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
                    double re_1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of complex number.");
                    double im_1 = Convert.ToDouble(Console.ReadLine());
                    number_1 = new Complex(re_1, im_1);
                    continue;
                case '1':
                    Console.WriteLine("Enter a real part of second complex number.");
                    double re_2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    double im_2 = Convert.ToDouble(Console.ReadLine());
                    number_2 = new Complex(re_2, im_2);
                    Complex sum = number_1 + number_2;
                    Complex.Print(sum);
                    continue;
                case '2':
                    Console.WriteLine("Enter a real part of second complex number.");
                    re_2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    im_2 = Convert.ToDouble(Console.ReadLine());
                    number_2 = new Complex(re_2, im_2);
                    Complex difference = number_1 - number_2;
                    Complex.Print(difference);
                    continue;
                case '3':
                    Console.WriteLine("Enter a real part of second complex number.");
                    re_2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    im_2 = Convert.ToDouble(Console.ReadLine());
                    number_2 = new Complex(re_2, im_2);
                    Complex product = number_1 - number_2;
                    Complex.Print(product);
                    continue;
                case '4':
                    Console.WriteLine("Enter a real part of second complex number.");
                    re_2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter a imaginary part of second complex number.");
                    im_2 = Convert.ToDouble(Console.ReadLine());
                    number_2 = new Complex(re_2, im_2);
                    Complex division = number_1 - number_2;
                    Complex.Print(division);
                    continue;
                case '5':
                    double absolut = Complex.ComplexModule(number_1);
                    Console.WriteLine(absolut);
                    continue;
                case '6':
                    double argument = Complex.ComplexArgument(number_1);
                    Console.WriteLine(argument);
                    continue;
                case '7':
                    Complex.Print_Re(number_1); 
                    Console.Write("+");
                    Complex.Print_Im(number_2);
                    continue;
                case '8':
                    Complex.Print_Re(number_1);
                    continue;
                case '9':
                    Complex.Print_Im(number_2);
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