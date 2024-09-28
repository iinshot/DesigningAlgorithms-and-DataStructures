using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVectorLib
{
    public class MyVector<T>
    {
        private int elementCount;
        private int capacityIncrement;
        T[] elementData;

        // 1
        public MyVector(int initialCapacity, int initialcapacityIncrement)
        {
            elementData = new T[initialCapacity];
            elementCount = initialCapacity;
            capacityIncrement = initialcapacityIncrement;
        }

        // 2
        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            capacityIncrement = 0;
        }

        // 3
        public MyVector()
        {
            elementData = null;
            capacityIncrement = 0;
            elementCount = 10;
        }

        // 4
        public MyVector(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            elementCount = array.Length;
        }

        // 5
        public void Add(T element)
        {
            if (elementCount == elementData.Length)
            {
                T[] array = new T[(int)(elementData.Length * 1.5) + 1];
                for (int i = 0; i < elementCount; i++)
                    array[i] = elementData[i];
                elementData = array;
            }
            elementData[elementCount] = element;
            elementCount++;
        }

        // 6
        public void AddAll(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

        // 7
        public void Clear()
        {
            elementData = null;
            elementCount = 0;
        }

        public bool Contains(object element)
        {
            for (int i = 0; i < elementCount; i++)
                if (elementData[i].Equals(element))
                    return true;
            return false;
        }
        // 8
        public bool ContainsAll(T[] array)
        {
            foreach (T item in array)
                for (int i = 0; i < elementCount; i++)
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
                for (int i = 0; i < elementCount; i++)
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
                elementCount--;
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
            T[] newArray = new T[elementCount];
            int newSize = 0;
            foreach (T item in array)
                for (int i = 0; i < elementCount; i++)
                    if (item.Equals(elementData[i]))
                    {
                        newArray[newSize] = elementData[i];
                        newSize++;
                    }
            elementData = newArray;
            elementCount = newSize;
        }

        // 13
        public int Size()
        {
            return elementCount;
        }

        // 14
        public object[] ToArray()
        {
            object[] array = new object[elementCount];
            for (int i = 0; i < elementCount; i++)
                array[i] = elementData[i];
            return array;
        }

        // 15
        public void ToArray(T[] array)
        {
            if (array == null) ToArray();
            for (int i = 0; i < elementCount && i < array.Length; i++)
                array[i] = (T)elementData[i];
        }

        // 16
        public void AddInd(int index, T element)
        {
            if (index >= elementCount)
            {
                Add(element);
                return;
            }
            T[] array = new T[elementCount + 1];
            for (int i = 0, j = 0; i <= elementCount; i++, j++)
            {
                if (i == index)
                {
                    array[j] = element;
                    i++;
                }
                array[j] = elementData[j];
            }
            elementData = array;
            elementCount++;
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
            if (index >= elementCount || index < 0)
                throw new ArgumentOutOfRangeException();
            return elementData[index];
        }

        // 19
        public int IndexOf(object element)
        {
            for (int i = 0; i < elementCount; i++)
                if (element.Equals(elementData[i]))
                    return i;
            return -1;
        }

        // 20
        public int LastIndexOf(object element)
        {
            int index = -1;
            for (int i = 0; index < elementCount; i++)
                if (element.Equals(elementData[i]))
                    index = i;
            return -1;
        }

        // 21
        public T Remove(int index)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            Remove(element);
            return element;
        }

        // 22
        public void Set(int index, T element)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            if (element == null)
                throw new ArgumentNullException();
            elementData[index] = element;
        }

        // 23
        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= elementCount)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= elementCount)
                throw new ArgumentOutOfRangeException("toIndex");
            MyVector<T> list = new MyVector<T>(toIndex - fromIndex);
            for (int i = 0; i < list.elementCount; i++)
                list.Set(i, elementData[i + fromIndex]);
            return list;
        }

        public void Print()
        {
            for (int i = 0; i < elementCount; i++)
                Console.WriteLine($"{elementData[i]} ");
            Console.WriteLine();
        }
    }
}
