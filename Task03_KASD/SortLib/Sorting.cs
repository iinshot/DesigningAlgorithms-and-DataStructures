using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortLib
{
    public class TreeNode
    {
        public int value { get; set; }
        public TreeNode(int key)
        {
            value = key;
        }
        public TreeNode right { get; set; }
        public TreeNode left { get; set; }

        public void InsertNode(TreeNode root)
        {
            if (root.value < value)
            {
                if (left == null) left = root;
                else left.InsertNode(root);
            }
            else
            {
                if (right == null) right = root;
                else right.InsertNode(root);
            }
        }

        public int[] TransformToArray(List<int> elements = null)
        {
            if (elements == null) elements = new List<int>();
            if (left != null) left.TransformToArray(elements);
            elements.Add(value);
            if (right != null) right.TransformToArray(elements);
            return elements.ToArray();
        }

    }
    public static class Sorts
    {
        // support method

        // converting subarray to a heap (HeapSort)
        public static void ConvertingToHeap(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && array[left] > array[largest])
                largest = left;
            if (right < n && array[right] > array[largest])
                largest = right;
            if (largest != i)
            {
                int temp = array[i];
                array[i] = array[largest];
                array[largest] = temp;
                ConvertingToHeap(array, n, largest);
            }
        }

        // for separation array (QuickSort)
        public static int Separation(int[] array, int start, int stop, bool swap = false)
        {
            int left = start;
            int support = array[left];
            int index = 0;
            if (start + 1 >= stop) return left;
            for (int i = start + 1; i < stop; i++)
            {
                if ((!swap && array[i] < support) || (swap && array[i] > support))
                {
                    int temp = array[i];
                    array[i] = array[left];
                    array[left] = temp;
                    left++;
                }
                if (array[i] == support)
                    index = i;
                else if (array[left] == support)
                    index = left;
            }
            array[index] = array[left];
            array[left] = support;
            return left;
        }
        // second part of quick sort (QuickSort)
        public static void SecondPart(this int[] array, int left, int right, bool swap = false)
        {
            if (left < right)
            {
                int dot = Separation(array, left, right, swap);
                SecondPart(array, left, dot, swap);
                SecondPart(array, dot + 1, right, swap);
            }
        }

        // for get digit of number (RadixSort)
        public static int GetDigit(int number, int place)
        {
            return (number / place) % 10;
        }
        // support counting sort (RadixSort)
        public static void SupportCountingSort(int[] array, int place, bool swap = false)
        {
            int countNums = array.Length;
            int[] output = new int[countNums];
            int[] count = new int[10];
            for (int i = 0; i < countNums; i++)
            {
                int digit = GetDigit(array[i], place);
                count[digit]++;
            }
            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];
            for (int i = countNums - 1; i >= 0; i--)
            {
                int digit = GetDigit(array[i], place);
                output[count[digit] - 1] = array[i];
                count[digit]--;
            }
            for (int i = 0; i < countNums; i++)
                array[i] = output[i];
            for (int i = 0; i < array.Length; i++)
            {
                if (swap)
                {
                    array[i] = output[array.Length - i - 1];
                    continue;
                }
                array[i] = output[i];
            }
        }

        // for sort bitonic sequence (BitonicSort)
        public static void BitonicSequenceSort(int[] array, int low, int count, bool upward)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                    if (upward ? array[i] > array[i + k] : array[i] < array[i + k])
                    {
                        int temp = array[i];
                        array[i] = array[i + k];
                        array[i + k] = temp;
                    }
                BitonicSequenceSort(array, low, k, upward);
                BitonicSequenceSort(array, low + k, k, upward);
            }
        }
        // for create bitonic sequence (BitonicSort)
        public static void BitonicSequenceCreate(int[] array, int low, int count, bool upward)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSequenceCreate(array, low, k, true);
                BitonicSequenceCreate(array, low + k, k, false);
                BitonicSequenceSort(array, low, count, upward);
            }
        }

        // for merge two subarrays (MergeSort)
        public static void MergeSubarrays(int[] array, int left, int mid, int right, bool swap)
        {
            int countNums1 = mid - left + 1;
            int countNums2 = right - mid;
            int[] leftArray = new int[countNums1];
            int[] rightArray = new int[countNums2];
            for (int i = 0; i < countNums1; i++)
                leftArray[i] = array[left + i];
            for (int i = 0; i < countNums2; i++)
                rightArray[i] = array[mid + i + 1];
            int j = 0, z = 0;
            while (j < countNums1 && z < countNums2)
            {
                if ((!swap && leftArray[j] < rightArray[z]) || (swap && leftArray[j] > rightArray[z]))
                {
                    array[left] = leftArray[j];
                    j++;
                }
                else
                {
                    array[left] = rightArray[z];
                    z++;
                }
                left++;
            }
            while (j < countNums1)
            {
                array[left] = leftArray[j];
                j++;
                left++;
            }
            while (z < countNums2)
            {
                array[left] = rightArray[z];
                z++;
                left++;
            }
        }
        // second part of merge sort (MergeSort)
        public static void Merge(this int[] array, int left, int right, bool swap = false)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                Merge(array, left, mid, swap);
                Merge(array, mid + 1, right, swap);
                MergeSubarrays(array, left, mid, right, swap);
            }
        }

        // main sorts
        public static int[] BubbleSort(int[] array, bool swap = false)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (!swap)
                    {
                        if (array[j] > array[j + 1])
                        {
                            int temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                        continue;
                    }
                    if (array[j] < array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            return array;
        }
        public static int[] ShakerSort(int[] array, bool swap = false)
        {
            int left = 0;
            int right = array.Length;
            bool isSwap = true;
            while (isSwap)
            {
                isSwap = false;
                for (int i = left; i < right - 1; i++)
                    if ((swap && array[i] < array[i + 1]) || (!swap && array[i] > array[i + 1]))
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isSwap = true;
                    }
                --right;
                if (!isSwap) break;
                for (int i = right - 1; i >= left; i--)
                    if ((swap && array[i] < array[i + 1]) || (!swap && array[i] > array[i + 1]))
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isSwap = true;
                    }
                ++left;
            }
            return array;
        }
        public static int[] CombSort(int[] array, bool swap = false)
        {
            double k = 1.2473309;
            int countNums = array.Length;
            bool isSwap = true;
            while (countNums != 1 || isSwap)
            {
                countNums = (int)(countNums / k);
                if (countNums < 1) countNums = 1;
                isSwap = false;
                for (int i = 0; i + countNums < array.Length; i++)
                    if ((!swap && array[i] > array[i + countNums]) || (swap && array[i] < array[i + countNums]))
                    {
                        int temp = array[i];
                        array[i] = array[i + countNums];
                        array[i + countNums] = temp;
                        isSwap = true;
                    }
            }
            return array;
        }
        public static int[] InsertionSort(int[] array, bool swap = false)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int swapIndex = i;
                while (swapIndex > 0 && ((!swap && array[swapIndex] < array[swapIndex - 1]) || (swap && array[swapIndex] > array[swapIndex - 1])))
                {
                    int temp = array[swapIndex];
                    array[swapIndex] = array[swapIndex - 1];
                    array[swapIndex - 1] = temp;
                    swapIndex--;
                }
            }
            return array;
        }
        public static int[] ShellSort(int[] array, bool swap = false)
        {
            int countNums = array.Length;
            for (int i = countNums / 2; i > 0; i /= 2)
                for (int j = i; j < countNums; j++)
                {
                    int temp = array[j];
                    int z;
                    for (z = j; z >= i && ((!swap && array[z - i] > temp) || (swap && array[z - i] < temp)); z -= i)
                        array[z] = array[z - i];
                    array[z] = temp;
                }
            return array;
        }
        public static int[] TreeSort(int[] array, bool swap = false)
        {
            TreeNode root = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++) root.InsertNode(new TreeNode(array[i]));
            int[] supportArray = root.TransformToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (swap)
                {
                    array[i] = supportArray[array.Length - 1 - i];
                    continue;
                }
                array[i] = supportArray[i];
            }
            return array;
        }
        public static int[] GnomeSort(int[] array, bool swap = false)
        {
            int left = 0;
            int right = array.Length;
            while (left < right)
            {
                if (left == 0) left++;
                if ((!swap && array[left - 1] <= array[left]) || (swap && array[left - 1] >= array[left])) left++;
                else
                {
                    int temp = array[left - 1];
                    array[left - 1] = array[left];
                    array[left] = temp;
                    left--;
                }
            }
            return array;
        }
        public static int[] SelectionSort(int[] array, bool swap = false)
        {
            int countNums = array.Length;
            for (int i = 0; i < countNums; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < countNums; j++)
                    if ((!swap && array[j] < array[minIndex]) || (swap && array[j] > array[minIndex]))
                        minIndex = j;
                if (minIndex != i)
                {
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
            return array;
        }
        public static int[] HeapSort(int[] array, bool swap = false)
        {
            int countNums = array.Length;
            for (int i = countNums / 2 - 1; i >= 0; i--)
                ConvertingToHeap(array, countNums, i);
            for (int i = countNums - 1; i >= 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                ConvertingToHeap(array, i, 0);
            }
            if (swap)
            {
                for (int i = 0; i < countNums / 2; i++)
                {
                    int temp = (int)array[i];
                    array[i] = array[countNums - i - 1];
                    array[countNums - i - 1] = temp;
                }
            }
            return array;
        }
        public static int[] QuickSort(int[] array, bool swap = false)
        {
            SecondPart(array, 0, array.Length, swap);
            return array;
        }
        public static int[] MergeSort(int[] array, bool swap = false)
        {
            Merge(array, 0, array.Length - 1, swap);
            return array;
        }
        public static int[] CountingSort(int[] array, bool swap = false)
        {
            int maxNum = array[0];
            foreach (int i in array) maxNum = Math.Max(maxNum, i);
            int[] count = new int[maxNum + 1];
            foreach (int i in array)
                count[i]++;
            for (int i = 1; i <= maxNum; i++)
                count[i] += count[i - 1];
            int[] answer = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                answer[count[array[i]] - 1] = array[i];
                count[array[i]]--;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (swap)
                {
                    array[i] = answer[array.Length - i - 1];
                    continue;
                }
                array[i] = answer[i];
            }
            return answer;
        }
        public static int[] RadixSort(int[] array, bool swap = false)
        {
            int max = array.Max();
            for (int place = 1; max / place > 0; place *= 10)
                SupportCountingSort(array, place, swap);
            return array;
        }
        public static int[] BitonicSort(int[] array, bool swap = false)
        {
            BitonicSequenceCreate(array, 0, array.Length, !swap);
            return array;
        }
    }
    public static class Generate
    {
        public static int[] GenerateArray(int size)
        {
            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(0, 1000);
            }
            return array;
        }
        public static int[] GenerateSubArrays(int size)
        {
            Random random = new Random();
            int module = random.Next(0, size);
            int newSize = random.Next(2, size) % module;
            if (newSize < 2) newSize = 2;
            int[] array = new int[size];
            int countArray = 0, i = 0;
            while (i < size)
            {
                int baseElement = 0;
                int exp = random.Next(0, 1000);
                countArray++;
                while (i < size && i < countArray * newSize)
                {
                    baseElement++;
                    array[i] = baseElement * exp;
                    i++;
                }
            }
            return array;
        }
        public static int[] GenerateBySwap(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = i;
            Random random = new Random();
            int countSwaps = random.Next(0, size / 3);
            for (int i = 0; i < countSwaps; i++)
            {
                int first = random.Next(0, array.Length - 1);
                int second = random.Next(0, array.Length - 1);
                int temp = array[first];
                array[first] = array[second];
                array[second] = temp;
            }
            return array;
        }
        public static int[] GenerateSwapAndRepeat(int size)
        {
            int[] array = GenerateBySwap(size);
            Random random = new Random();
            int repeatIndex = random.Next(0, array.Length - 1);
            int repeatCount = random.Next(0, array.Length / 3);
            while (repeatCount > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[repeatIndex])
                {
                    array[randomIndex] = array[repeatIndex];
                    repeatCount--;
                }
            }
            return array;
        }
    }
}