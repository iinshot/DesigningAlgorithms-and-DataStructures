﻿using HeapLib;
using System.Net.Quic;
public class MyPriorityQueue<T> where T : IComparable<T>
{
    private MyArrayList<T> queue;
    private int size;
    private Comparer<T> comparator;

    // support methods

    // for swapping
    private void Swap(int a, int b)
    {
        T temp1 = queue.Get(a);
        T temp2 = queue.Get(b);
        queue.Set(b, temp1);
        queue.Set(a, temp2);
    }

    // heapify
    private void Heapify(int i)
    {
        int parent = i;
        int leftChild;
        int rightChild;
        while (true)
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            if (rightChild < size && queue.Get(rightChild).CompareTo(queue.Get(parent)) > 0)
                parent = rightChild;
            if (leftChild < size && queue.Get(leftChild).CompareTo(queue.Get(parent)) > 0)
                parent = leftChild;
            if (parent == i)
                break;
            Swap(parent, i);
            i = parent;
        }
    }

    // 1
    public MyPriorityQueue()
    {
        queue = new MyArrayList<T>(11);
        size = 0;
    }

    // 2
    public MyPriorityQueue(T[] array)
    {
        queue = new MyArrayList<T>(array.Length);
        size = array.Length;
        for (int i = 0; i < array.Length; i++)
            queue.Add(array[i]);
        for (int i = size / 2; i >= 0; i--)
            Heapify(i);
    }

    // 3
    public MyPriorityQueue(int initialCapacity)
    {
        if (initialCapacity < 0)
            throw new ArgumentOutOfRangeException("Error in initialCapacity");
        queue = new MyArrayList<T>(initialCapacity);
        size = 0;
    }

    // 4
    public MyPriorityQueue(int initialCapacity, Comparer<T> thisComparator)
    {
        if (initialCapacity < 0)
            throw new ArgumentOutOfRangeException("Error in initialCapacity");
        queue = new MyArrayList<T>(initialCapacity);
        size = initialCapacity;
        comparator = thisComparator;
    }

    // 5
    public MyPriorityQueue(MyPriorityQueue<T> c)
    {
        T[] array = new T[size];
        for (int i = 0; i < c.size; i++)
            array[i] = c.queue.Get(i);
        queue = new MyArrayList<T>(c.size);
        size = c.size;
        for (int i = 0; i < c.size; i++)
            queue.Add(array[i]);
        for (int i = size / 2; i >= 0; i--)
            Heapify(i);
    }

    // 6
    public void Add(T element)
    {
        queue.Add(element);
        size++;
        for (int i = size / 2; i >= 0; i--)
            Heapify(i);
    }

    // 7
    public void AddAll(T[] array)
    {
        foreach(T item in array)
            queue.Add(item);
    }

    // 8
    public void Clear()
    {
        queue.Clear();
        size = 0;
    }

    // 9, 10, 11, 15, 16, 17, 18
    public bool Contains(T element) => queue.Contains(element);
    public bool IsEmpty() => queue.IsEmpty();
    public bool ContainsAll(T[]array) => queue.ContainsAll(array);
    public int Size() => queue.Size();
    public T[] ToArray() => queue.ToArray();
    public T[] ToArray(T[] array) => queue.ToArray(array);
    public T Element() => queue.Get(0);

    // 12
    public void Remove(object element)
    {
        queue.Remove(element);
        size--;
        for (int i = size / 2 - 1; i >= 0; i--)
            Heapify(i);
    }

    // 13
    public void RemoveAll(T[] array)
    {
        foreach(T item in array)
            queue.Remove(item);
    }

    // 14
    public void RetainAll(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            queue.Set(i, array[i]);
        size = array.Length;
    }

    // 19
    public bool Offer(T element)
    {
        if (size == queue.Capacity())
            return false;
        queue.Add(element);
        for (int i = size / 2 - 1; i >= 0; i--)
            Heapify(i);
        return false;
    }

    // 20
    public T Peek()
    {
        if (size == 0)
            throw new IndexOutOfRangeException("Empty queue");
        else return queue.Get(0);
    }

    // 21
    public T Poll()
    {
        if (size == 0)
            throw new IndexOutOfRangeException("Empty queue");
        T element = queue.Get(0);
        queue.Remove(queue.Get(0));
        for (int i = size / 2; i >= 0; i--)
            Heapify(i);
        return element;
    }

    public void Print()
    {
        for (int i = 0; i < size; i++)
            Console.WriteLine(queue.Get(i));
    }
}
public class Program
{
    static void Main(string[] args)
    {
        int[] array = { 1, 8, -60, 23, 12, -2, 5, 9 };
        MyPriorityQueue<int> priorityQueue = new MyPriorityQueue<int>(array.Length);
        for (int i = 0; i <  array.Length; i++)
            priorityQueue.Add(array[i]);
        priorityQueue.Print();
        Console.WriteLine();
        priorityQueue.Remove(8);
        priorityQueue.Offer(-60);
        priorityQueue.Print();
    }
}