using MyDequeLib;
public class Sorts
{
    public int DigitCount(string line)
    {
        int count = 0;
        string[] path = line.Split(' ');
        for (int i = 0; i < path.Length; i++)
            count += path[i].Length;
        return count;
    }
    public void AddLine(int n)
    {
        MyArrayDeque<string> deque = new MyArrayDeque<string>();
        string pathIn = "input.txt";
        string pathOut = "sorted.txt";
        StreamReader sr = new StreamReader(pathIn);
        StreamWriter sw = new StreamWriter(pathOut);
        string? line = sr.ReadLine();
        deque.Add(line);
        if (line == null)
            Console.WriteLine("Path is empty.");
        while (line != null)
        {
            line = sr.ReadLine();
            if (line != null)
            {
                if (DigitCount(line) > DigitCount(deque.GetFirst()))
                    deque.AddLast(line);
                else deque.AddFirst(line);
            }
        }
        for (int i = 0; i < deque.Size(); i++)
            sw.WriteLine(deque.GetInd(i));
        sw.Close();
        for (int i = 0; i < deque.Size(); i++)
        {
            string path = deque.GetInd(i);
            string[] elements = path.Split(' ');
            if (elements.Length - 1 > n)
                deque.Remove(path);
        }
        deque.Print();
    }
}
public class Program
{
    static void Main(string[] args)
    {
        Sorts sorting = new Sorts();
        string n = Console.ReadLine();
        int n2 = Convert.ToInt32(n);
        sorting.AddLine(n2);
    }
}