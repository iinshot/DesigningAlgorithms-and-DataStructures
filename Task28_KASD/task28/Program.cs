using Library;
public class Program
{
    static void Main(string[] args)
    {
        int[] array = { 4, 3, 1, 7, 5, 9, 8, 16, 15 };
        MyArrayList<int> list = new MyArrayList<int>(array);
        var iter = list.IteratorList();
        while (iter.HasNext())
        {
            iter.Next();
            Console.WriteLine(iter.Cursor);
        }
    }
}