using QueueLib;
using System.Diagnostics;
public class Bid : IComparable<Bid>
{
    private int priority;
    public int Priority
    {
        get { return priority; }
        set { priority = value; }
    }
    private int number { get; set; }
    public int Number
    {
        get { return number; }
        set { number = value; }
    }
    private int numberStep { get; set; }
    public int NumberStep
    {
        get { return numberStep;  }
        set { numberStep = value; }
    }
    public Bid(int priority, int number, int numberStep)
    {
        this.priority = priority;
        this.number = number;
        this.numberStep = numberStep;
    }
    public int CompareTo(Bid other) => priority.CompareTo(other.priority);
}
public class Program
{
    static void Main(string[] args)
    {
        string path = "log.txt";
        MyPriorityQueue<Bid> order = new MyPriorityQueue<Bid>();
        int n = Convert.ToInt32(Console.ReadLine());
        int count = 0;
        Stopwatch stopWatch = new Stopwatch();
        StreamWriter streamWriter = new StreamWriter(path);
        for (int i = 0; i < n; i++)
        {
            Random random = new Random();
            int num = random.Next(1, 11);
            for (int j = 0; j < num; j++)
            {
                int part = random.Next(1, 11);
                Bid array = new Bid(part, j, i);
                order.Add(array);
                streamWriter.WriteLine($"Add: {array.Number} {array.Priority} {array.NumberStep} ");
                count++;
            }
        }
        for (int i = 0; i < count; i++)
        {
            Bid temp = order.Peek();
            streamWriter.WriteLine($"Remove: {temp.Number} {temp.Priority} {temp.NumberStep} ");
            order.Remove(order.Peek());
        }
        stopWatch.Stop();
        TimeSpan expire = stopWatch.Elapsed;
        streamWriter.WriteLine($"Running time: {expire.TotalSeconds} seconds.");
        streamWriter.Close();
    }
}