using MyVectorLib;
public class MyStack<T> : MyVector<T>
{
    private int top;
    private MyVector<T> stack;
    public MyStack()
    {
        top = -1;
        stack = new MyVector<T>(1);
    }

    // 1
    public void Push(T item)
    {
        top++;
        stack.Add(item);
    }

    // 2
    public void Pop()
    {
        stack.Remove(top);
        top--;
    }

    // 3
    public T Peek()
    {
        if (top == -1)
            throw new Exception("Stack if empty.");
        else return stack.Get(top);
    } 

    // 4
    public bool Empty()
    {
        if (stack.Size == null)
            return false;
        return true;
    }

    // 5
    public int Search(T item)
    {
        if (stack.IndexOf(item) == -1)
            return -1;
        return top-stack.IndexOf(item) + 1;
    }
    
    public void PrintStack()
    {
        for (int i = 0; i <= top; i++)
            Console.WriteLine(stack.Get(i));
    }
}
class Program
{
    static void Main(string[] args)
    {
        MyStack<int> stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Console.WriteLine(stack.Peek());
        stack.Pop();
        Console.WriteLine(stack.Search(5));
        Console.WriteLine();
        stack.PrintStack();
    }
}