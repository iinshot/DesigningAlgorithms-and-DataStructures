using Library;
public class Program
{
    static void Main(string[] args)
    {
        int[] array = { 4, 3, 1, 7, 5, 9, 8, 16, 15 };
        MyLinkedList<int> list = new MyLinkedList<int>(array);
        var item = list.ListIterator();
        while (item.HasNext())
        {
            item.Next();
            Console.WriteLine(item.Cursor);
        }
    }
}