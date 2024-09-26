using MyVectorLib;
public class Program
{
    static string inPath = "input.txt";
    static string outPath = "output.txt";
    static StreamReader sr = new StreamReader(inPath);
    static StreamWriter sw = new StreamWriter(outPath);
    static MyVector<string> Ip()
    {
        string line = sr.ReadLine();
        if (line == null)
            Console.WriteLine("String is empty");
        MyVector<string> vector = new MyVector<string>(10);
        while (line != null)
        {
            string[]arrayOfIp = line.Split(' ');
            foreach(string adress in arrayOfIp)
            {
                bool isIpAdress = true;
                // choose blocks of three numbers
                int[]blockIp = adress.Split(".").Select(x => Convert.ToInt32(x)).ToArray();
                foreach(int item in blockIp)
                {
                    // block need be: 0 < block < 256
                    if (item > 255 || item < 0)
                        isIpAdress = false;
                    if (isIpAdress && blockIp.Length == 4)
                        vector.Add(adress);
                }
                line = sr.ReadLine();
            }
        }
        sr.Close();
        return vector;
    }
    static void WriteIpToFile(MyVector<string> ip)
    {
        for (int i = 0; i < ip.Size(); i++)
        {
            string adressIp = ip.Get(i);
            sw.WriteLine(adressIp);
        }
        sw.Close();
    }
    static void Main(string[] args)
    {
        MyVector<string> ip = new MyVector<string>(10);
        ip = Ip();
        WriteIpToFile(ip);
    }
}