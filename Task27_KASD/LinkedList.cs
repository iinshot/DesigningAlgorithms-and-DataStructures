using System.Diagnostics;

namespace LinkedList
{
    public class MyLinkedList<T>
    {
        public class MyItr : MyIterator2<T>
        {
            MyLinkedList<T> list;
            MyLLElement<T> element = null;
            int index = 0;
            T cursor;
            public T Cursor => cursor;
            public MyItr(MyArrayList<T> array, int curIndex = 0)
            {
                list = array;
                curIndex = index;
                if (index != 0)
                {
                    element = list.first;
                    for (int i = 0; i < index; i++)
                        element = element.next;
                }
            }
            public bool HasNext()
            {
                if (element == null && index == 0 && list.size > 0)
                    return true;
                if (element == null && index == 0 && list.size == 0)
                    return false;
                if (element.next == null)
                    return false;
                return true;
            }
            public T Next()
            {
                if (element == null && list.size > 0)
                {
                    element = list.first;
                    cursor = element.value;
                    return element.value;
                }
                element = element.next;
                cursor = element.value;
                index++;
                if (element == null)
                    return Cursor;
                return element.value;
            }
            public bool HasPrevious()
            {
                if (element != null && element.prev == null)
                    return false;
                return true;
            }
            public T Previous()
            {
                if (element.prev != null)
                    element = element.prev;
                index--;
                return element.value;
            }
            public int PreviousIndex() => index - 1;
            public void Remove()
            {
                if (element == null)
                    return;
                MyLLElement<T> nextElement = element.next;
                MyLLElement<T> prevElement = element.prev;
                if (nextElement != null && prevElement != null)
                {
                    nextElement.prev = prevElement;
                    prevElement.next = nextElement;
                }
                else if (nextElement == null && prevElement != null)
                    prevElement.next = null;
                else if (nextElement != null && prevElement == null)
                    nextElement.prev = null;
                else if (nextElement == null && prevElement == null)
                {
                    cursor = element.value;
                    element = null;
                }
                list.size--;
                if (index != 0)
                    index--;
            }
            public void Set(T obj)
            {
                obj.value = element;
            }
            public void Add(T obj)
            {
                MyLLElement<T> newElement = new MyLLElement<T>();
                newElement.value = element;
                MyLLElement<T> nextElement = element.next;
                element.next = newElement;
                nextElement.prev = newElement;
                newElement.next = nextElement;
                newElement.prev = element;
                list.size++;
            }

        }
        private class MyLLElement<T>
        {
            public T value;
            public MyLLElement<T> next;
            public MyLLElement<T> prev;
            public MyLLElement(T element)
            {
                next = null;
                prev = next;
                value = element;
            }

        }
        private MyLLElement<T> first;
        private MyLLElement<T> last;
        private int size;

        // 1
        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }

        // 2
        public MyLinkedList(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

        // 3
        public MyLinkedList(int capacity)
        {
            first = null;
            last = null;
            size = capacity;
        }

        // 4
        public void Add(T item)
        {
            MyLLElement<T> element = new MyLLElement<T>(item);
            if (size == 0)
            {
                first = element;
                last = element;
            }
            else
            {
                last.next = element;
                element.prev = last;
                last = element;
            }
            size++;
        }

        // 5
        public void AddAll(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

        // 6
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }

        // 7
        public bool Contains(T item)
        {
            MyLLElement<T> step = first;
            while (step != null)
            {
                if (step.value.Equals(item))
                    return true;
                step = step.next;
            }
            return false;
        }

        // 8
        public bool ContainsAll(T[] array)
        {
            bool[] newArray = new bool[array.Length];
            MyLLElement<T> step = first;
            while (step != null)
            {
                int cnt = 0;
                if (step.Equals(array[cnt]))
                    newArray[cnt] = true;
                cnt++;
                step = step.next;
            }
            for (int i = 0; i < newArray.Length; i++)
                if (!newArray[i])
                    return false;
            return true;
        }

        // 10
        public void Remove(T item)
        {
            if (Contains(item))
            {
                if (first.value.Equals(item))
                {
                    first = first.next;
                    size--;
                    return;
                }
                MyLLElement<T> step = first;
                while (step != null)
                {
                    if (step.next.value.Equals(item))
                    {
                        step = step.next;
                        size--;
                        return;
                    }
                    else
                        step = step.next;
                }
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
            T[] newArray = new T[array.Length];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                int fl = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (Get(i).Equals(array[j]))
                    {
                        fl = 0;
                        break;
                    }
                    else
                        fl = 1;
                }
                if (fl == 1)
                    Remove(Get(i));
            }
        }

        // 14
        public T[] ToArray()
        {
            T[] newArray = new T[size];
            for (int i = 0; i < size; i++)
                newArray[i] = Get(i);
            return newArray;
        }

        // 15
        public T[] ToArray(T[] array)
        {
            if (array == null)
                return ToArray();
            else
            {
                T[] newArray = new T[array.Length + size];
                for (int i = 0; i < array.Length; i++)
                    newArray[i] = array[i];
                for (int i = array.Length; i < newArray.Length; i++)
                    newArray[i] = Get(i);
                return newArray;
            }
        }

        // 16
        public void Add(int index, T item)
        {
            if (index == 0)
            {
                MyLLElement<T> step = new MyLLElement<T>(item);
                step.next = first;
                first.prev = step;
                first = step;
                return;
            }
            else if (index == size - 1)
            {
                MyLLElement<T> step = new MyLLElement<T>(item);
                step.prev = last;
                last.next = step;
                last = step;
                return;
            }
            else
            {
                MyLLElement<T> step = new MyLLElement<T>(item);
                step = first;
                int cnt = 0;
                while (cnt != index)
                {
                    step = step.next;
                    cnt++;
                }
                if (cnt == index)
                {
                    MyLLElement<T> element = new MyLLElement<T>(item);
                    element.next = step;
                    element.prev = step.prev;
                    step.prev.next = element;
                    step.prev = element;
                }
            }
        }

        // 17
        public void AddAll(int index, T[] array)
        {
            foreach (T item in array)
                Add(index, item);
        }

        // 18
        public T Get(int index)
        {
            int current = 0;
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == size - 1)
                return last.value;
            if (index == 0)
                return first.value;
            MyLLElement<T> step = first;
            while (current != index)
            {
                step = step.next;
                current++;
            }
            return step.value;
        }

        // 19
        public int IndexOf(T item)
        {
            int i = 0;
            MyLLElement<T> step = new MyLLElement<T>(item);
            step = first;
            while (step != null)
            {
                if (step.value.Equals(item))
                    return i;
                i++;
                step = step.next;
            }
            return -1;
        }

        // 20
        public int LastIndexOf(T item)
        {
            int i = 0;
            int retI = -1;
            MyLLElement<T> step = new MyLLElement<T>(item);
            step = first;
            while (step != null)
            {
                if (step.value.Equals(item))
                    retI = i;
                i++;
                step = step.next;
            }
            return retI;
        }

        // 21
        public T Remove(int index)
        {
            T item = Get(index);
            Remove(item);
            return item;
        }

        // 22
        public void Set(int index, T item)
        {
            MyLLElement<T> step = new MyLLElement<T>(item);
            step = first;
            int ind = 0;
            while (ind != index)
            {
                ind++;
                step = step.next;
            }
            step.value = item;
        }

        // 23
        public T[] SubList(int fromIndex, int toIndex)
        {
            T[] array = new T[toIndex - fromIndex + 1];
            int index1 = 0;
            int index2 = 0;
            MyLLElement<T> step = new MyLLElement<T>(first.value);
            step = first;
            while (index1 != fromIndex)
            {
                step = step.next;
                index1++;
            }
            while (index1 <= toIndex)
            {
                index1++;
                index2++;
                array[index2] = step.value;
                step = step.next;
            }
            return array;
        }

        // 25
        public bool Offer(T item)
        {
            Add(item);
            if (Contains(item))
                return true;
            return false;
        }

        // 26
        public T Peek()
        {
            if (first == null)
                throw new NullReferenceException();
            return first.value;
        }

        // 27
        public T Poll()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        // 30
        public T GetFirst()
        {
            if (first == null)
                throw new NullReferenceException();
            return first.value;
        }

        // 31
        public T GetLast()
        {
            if (first == null)
                throw new NullReferenceException();
            return last.value;
        }

        // 32
        public bool OfferFirst(T item)
        {
            AddFirst(item);
            if (Contains(item))
                return true;
            return false;
        }

        // 33
        public bool OfferLast(T item)
        {
            AddLast(item);
            if (Contains(item))
                return true;
            return false;
        }

        // 34
        public T Pop()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        // 35
        public T PeekFirst()
        {
            if (size == 0)
                throw new Exception();
            return first.value;
        }

        // 36
        public T PeekLast()
        {
            if (size == 0)
                throw new Exception();
            return last.value;
        }

        // 37
        public T PollFirst()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        // 38
        public T PollLast()
        {
            T item = last.value;
            Remove(item);
            return item;
        }

        // 39
        public T RemoveLast()
        {
            T item = last.value;
            Remove(item);
            return item;
        }

        // 40
        public T RemoveFirst()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        // 41
        public bool RemoveLastOccurence(T item)
        {
            int index = LastIndexOf(item);
            if (index != -1)
            {
                Remove(index);
                return true;
            }
            return false;
        }

        // 42
        public bool RemoveFirstOccurence(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                Remove(index);
                return true;
            }
            return false;
        }

        // 9, 13, 24, 28, 29, 35
        public bool IsEmpty() => size == 0;
        public int Size() => size;
        public T Element() => first.value;
        public void AddFirst(T item) => Add(0, item);
        public void AddLast(T item) => Add(size - 1, item);
        public void Push(T item) => AddFirst(item);

        public void Print()
        {
            Console.WriteLine(this);
        }
        public override string ToString()
        {
            string path = "";
            MyLLElement<T> step = new MyLLElement<T>(first.value);
            step = first;
            while (step != null)
            {
                if (step.next != null)
                {
                    path += step.value + ", ";
                    step = step.next;
                }
                else
                {
                    path += step.value;
                    step = step.next;
                }
            }
            return path;
        }
    }
}