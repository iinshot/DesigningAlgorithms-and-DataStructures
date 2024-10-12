using System;
using MyVector;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStackLib
{
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
            return stack.Get(top);
        }

        // 4
        public bool Empty()
        { 
            return stack.Size() == 0;
        }

        // 5
        public int Search(T item)
        {
            if (stack.IndexOf(item) == -1)
                return -1;
            return top - stack.IndexOf(item) + 1;
        }

        public void PrintStack()
        {
            for (int i = 0; i <= top; i++)
                Console.WriteLine(stack.Get(i));
        }
    }
}
