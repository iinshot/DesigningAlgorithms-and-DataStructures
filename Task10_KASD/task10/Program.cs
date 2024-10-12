using MyArrayLib;
public class Heap<T> where T : IComparable<T>
{
    public MyArrayList<T> heap = new MyArrayList<T>(10);
    private int length;

    // for swapping two values (helping method)
    private void Swapping(int a, int b)
    {
        T temp1 = heap.Get(a);
        T temp2 = heap.Get(b);
        heap.Set(b, temp1);
        heap.Set(a, temp2);
    }

    // for getting heap (helping method)
    public void ConvertingToHeap(int i)
    {
        int parent = i;
        int leftChild;
        int rightChild;
        while (true)
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            if (rightChild < length && heap.Get(rightChild).CompareTo(heap.Get(parent)) > 0)
                parent = rightChild;
            if (leftChild < length && heap.Get(leftChild).CompareTo(heap.Get(parent)) > 0)
                parent = leftChild;
            if (parent == i)
                break;
            Swapping(parent, i);
            i = parent;
        }
    }

    // for printing heap (helping method)
    public void Print()
    {
        for (int i = 0; i < length; i++)
            Console.WriteLine(heap.Get(i));
    }

    // 1
    public Heap(T[] array)
    {
        length = array.Length;
        for (int i = 0; i < length; i++)
        {
            heap.Add(array[i]);
        }
        for (int i = length / 2 - 1; i >= 0; i--)
            ConvertingToHeap(i);
    }

    // 2
    public T MaximumSearch() => heap.Get(0);

    // 3
    public T MaximumDelete()
    {
        T maximum = heap.Get(0);
        heap.Set(0, heap.Get(length - 1));
        length--;
        ConvertingToHeap(0);
        return maximum;
    }

    // 4
    public void KeyUp(int index, T newKey)
    {
        if (index > heap.Size() - 1)
            throw new IndexOutOfRangeException("index!");
        heap.Set(index, newKey);
        for (int i = length / 2; i >= 0; i--)
            ConvertingToHeap(i);
    }

    // 5
    public void AddToHeap(T element)
    {
        heap.Set(length, element);
        length++;
        for (int i = length / 2; i >= 0; i--)
            ConvertingToHeap(i);
    }

    // 6
    public void MergeHeaps(Heap<T> newHeap)
    {
        while (newHeap.length > 0)
        {
            T element = newHeap.MaximumDelete();
            AddToHeap(element);
        }
        for (int i = length / 2; i >= 0; i--)
            ConvertingToHeap(i);
    }
}
class Program
{
    static void Main(string[] args)
    {
        double[] array = { 1, 2, 6, 32, 11, -8, 9, -12 };
        Heap<double> heap = new Heap<double>(array);
        Console.WriteLine(heap.MaximumSearch());
        heap.Print();
        //for (int i = 0; i < array.Length; i++)
        //{
        //    heap.AddToHeap(array[i]);

        //}
        //Console.WriteLine();
        //heap.Print();
    }
}