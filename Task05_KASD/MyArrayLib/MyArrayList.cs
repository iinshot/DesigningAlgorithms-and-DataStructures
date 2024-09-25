using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayLib
{
    public class MyArrayList<T>
    {
        private int size;
        private T[] elementData;

        // 1
        public MyArrayList()
        {
            elementData = null;
            size = 0;
        }

        // 2
        public MyArrayList(T[] array)
        {
            elementData = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            size = array.Length;
        }

        // 3
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = capacity;
        }

        // 4
        public void AddElement(T element)
        {
            if (size == elementData.Length)
                Resize();
            elementData[size++] = element;
        }
        private void Resize()
        {
            T[] newArray = new T[elementData.Length * 2];
            Array.Copy(elementData, newArray, elementData.Length);
            elementData = newArray;
        }

        //5
        public void AddAll(T[] array)
        {
            foreach (T item in array)
                AddElement(item);
        }

        // 6
        public void Clear()
        {
            size = 0;
        }

        // 7
        public bool Contains(object element)
        {
            for (int i = 0; i < size; i++)
                if (elementData[i].Equals(element))
                    return true;
            return false;
        }

        // 8
        public bool ContainsAll(T[] array)
        {
            foreach (T item in array)
                for (int i = 0; i < size; i++)
                    if (elementData[i].Equals(item))
                        return false;
            return true;
        }

        // 9
        public bool IsEmpty()
        {
            if (elementData == null)
                return false;
            return true;
        }

        // 10
        public void Remove(object element)
        {
            if (Contains(element))
            {
                int flag = 0;
                for (int i = 0; i < size; i++)
                    if (flag == 0)
                    {
                        if (elementData[i].Equals(element))
                        {
                            elementData[i] = elementData[i + 1];
                            flag = 1;
                        }
                    }
                    else if (flag == 1)
                        elementData[i] = elementData[i + 1];
                size--;
            }
        }

        // 11
        public void RemoveAll(T[] array)
        {
            foreach (T item in array)
                Remove(item);
        }

        // 12
        public void RetainAll(T[] array)
        {
            T[] newArray = new T[size];
            int newSize = 0;
            foreach (T item in array)
                for (int i = 0; i < size; i++)
                    if (item.Equals(elementData[i]))
                    {
                        newArray[newSize] = elementData[i];
                        newSize++;
                    }
            elementData = newArray;
            size = newSize;
        }

        // 13
        public int Size()
        {
            return size;
        }

        // 14
        public object[] ToArray()
        {
            object[] array = new object[size];
            for (int i = 0; i < size; i++)
                array[i] = elementData[i];
            return array;
        }

        // 15
        public void ToArray(T[] array)
        {
            if (array == null) ToArray();
            for (int i = 0; i < size && i < array.Length; i++)
                array[i] = (T)elementData[i];
        }

        // 16
        public void AddInd(int index, T element)
        {
            if (index >= size)
            {
                AddElement(element);
                return;
            }
            T[] array = new T[size + 1];
            for (int i = 0, j = 0; i <= size; i++, j++)
            {
                if (i == index)
                {
                    array[j] = element;
                    i++;
                }
                array[j] = elementData[j];
            }
            elementData = array;
            size++;
        }

        // 17
        public void AddAllInd(int index, T[] array)
        {
            foreach (T item in array)
            {
                AddInd(index, item);
                index++;
            }
        }

        // 18
        public T Get(int index)
        {
            if (index >= size || index < 0)
                throw new ArgumentOutOfRangeException();
            return elementData[index];
        }

        // 19
        public int IndexOf(object element)
        {
            for (int i = 0; i < size; i++)
                if (element.Equals(elementData[i]))
                    return i;
            return -1;
        }

        // 20
        public int LastIndexOf(object element)
        {
            int index = -1;
            for (int i = 0; index < size; i++)
                if (element.Equals(elementData[i]))
                    index = i;
            return -1;
        }

        // 21
        public T Remove(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            Remove(element);
            return element;
        }

        // 22
        public void Set(int index, T element)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;
        }

        // 23
        public MyArrayList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= size)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= size)
                throw new ArgumentOutOfRangeException("toIndex");
            MyArrayList<T> list = new MyArrayList<T>(toIndex - fromIndex);
            for (int i = 0; i < list.size; i++)
                list.AddElement(elementData[i]);
            return list;
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
                Console.WriteLine($"{elementData[i]} ");
            Console.WriteLine();
        }
    }
}
