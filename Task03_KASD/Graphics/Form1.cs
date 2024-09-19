using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using SortLib;
using ZedGraph;

namespace Graphics
{
    public partial class Form1 : Form
    {
        // for adding to txt file
        public void SetPath()
        {
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            pathArray = appDirectory + @"\array.txt";
            pathTime = appDirectory + @"\time.txt";
        }
        string pathArray;
        string pathTime;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int arrayIndex = -1;
        int groupIndex = -1;

        // for sum the time of sorting
        public void TimeOfSorting(Func<int, int[]> Generate, int size, bool swap, params Func<int[], bool, int[]>[] MethodsOfSort)
        {
            SetPath();
            double[] sumSpeedSort = new double[MethodsOfSort.Length];
            for (int i = 0; i < 20; i++)
            {
                int[] array = Generate(size);
                try
                {
                    StreamWriter sw = File.AppendText(pathArray);
                    sw.WriteLine("Unsorted array: " + (i + 1).ToString());
                    foreach (int j in array) 
                        sw.Write(j.ToString() + " ");
                    sw.WriteLine();
                    int[] sortedArray = null;
                    int index = 0;
                    foreach (Func<int[], bool, int[]> Method in MethodsOfSort)
                    {
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        sortedArray = Method(array, swap);
                        timer.Stop();
                        sumSpeedSort[index] += timer.ElapsedMilliseconds;
                        index++;
                    }
                    sw.WriteLine("Sorted array: " + (i + 1).ToString());
                    foreach (int j in sortedArray) sw.Write(j.ToString() + " ");
                    sw.WriteLine();
                    sw.Close();
                }catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            try
            {
                StreamWriter sw = File.AppendText(pathTime);
                for (int i = 0; i < sumSpeedSort.Length; i++)
                {
                    if (i == 0)
                    {
                        sw.Write(((double)(sumSpeedSort[i] / 20)).ToString());
                        continue;
                    }
                    sw.Write(" " + ((double)(sumSpeedSort[i] / 20)).ToString());
                }
                sw.WriteLine();
                sw.Close();
            }catch (Exception ex) { Console.WriteLine(ex); };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayIndex = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupIndex = comboBox2.SelectedIndex;
        }

        int check = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            SetPath();
            File.WriteAllText(pathTime, String.Empty);
            File.WriteAllText(pathArray, String.Empty);
            switch (arrayIndex)
            {
                case 0:
                    switch (groupIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10) 
                                TimeOfSorting(Generate.GenerateArray, size, false, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10) 
                                TimeOfSorting(Generate.GenerateArray, size, false, Sorts.BitonicSort, Sorts.ShellSort, Sorts.TreeSort);

                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10) 
                                TimeOfSorting(Generate.GenerateArray, size, false, Sorts.CombSort, Sorts.HeapSort, Sorts.QuickSort, Sorts.CountingSort, Sorts.MergeSort, Sorts.RadixSort);
                            check = 3;
                            break;
                    }
                    break;
                case 1:
                    switch (groupIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                TimeOfSorting(Generate.GenerateSubArrays, size, false, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                TimeOfSorting(Generate.GenerateSubArrays, size, false, Sorts.BitonicSort, Sorts.ShellSort, Sorts.TreeSort);
                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                TimeOfSorting(Generate.GenerateSubArrays, size, false, Sorts.CombSort, Sorts.HeapSort, Sorts.QuickSort, Sorts.CountingSort, Sorts.MergeSort, Sorts.RadixSort);
                            check = 3;
                            break;
                    }
                    break;
                case 2:
                    switch (groupIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                TimeOfSorting(Generate.GenerateBySwap, size, false, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                TimeOfSorting(Generate.GenerateBySwap, size, false, Sorts.BitonicSort, Sorts.ShellSort, Sorts.TreeSort);
                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                TimeOfSorting(Generate.GenerateBySwap, size, false, Sorts.CombSort, Sorts.HeapSort, Sorts.QuickSort, Sorts.CountingSort, Sorts.MergeSort, Sorts.RadixSort);
                            check = 3;
                            break;
                    }
                    break;
                case 3:
                    switch (groupIndex)
                    {
                        case 0:
                            for (int size = 10; size <= Math.Pow(10, 4); size *= 10)
                                TimeOfSorting(Generate.GenerateSwapAndRepeat, size, false, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            check = 1;
                            break;
                        case 1:
                            for (int size = 10; size <= Math.Pow(10, 5); size *= 10)
                                TimeOfSorting(Generate.GenerateSwapAndRepeat, size, false, Sorts.BitonicSort, Sorts.ShellSort, Sorts.TreeSort);
                            check = 2;
                            break;
                        case 2:
                            for (int size = 10; size <= Math.Pow(10, 6); size *= 10)
                                TimeOfSorting(Generate.GenerateSwapAndRepeat, size, false, Sorts.CombSort, Sorts.HeapSort, Sorts.QuickSort, Sorts.CountingSort, Sorts.MergeSort, Sorts.RadixSort);
                            check = 3;
                            break;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<List<double>> list = new List<List<double>>();
            SetPath();
            try
            {
                StreamReader sr = new StreamReader(pathTime);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] lineArray = line.Split(' ');
                    List<double> time = new List<double>();
                    foreach (string s in lineArray)
                        time.Add(Convert.ToDouble(s));
                    list.Add(time);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Size of array";
            pane.YAxis.Title.Text = "Working time, m/s";
            pane.Title.Text = "The ratio between working time and array size";
            for (int i = 0; i < list[0].Count(); i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;
                for (int j = 0; j < list.Count(); j++)
                {
                    pointList.Add(x, list[j][i]);
                    x *= 10;
                }
                switch (check)
                {
                    case 1:
                        switch (i)
                        {
                            case 0:
                                pane.AddCurve("BubbleSort", pointList, Color.Black, SymbolType.Default);
                                break;
                            case 2:
                                pane.AddCurve("InsertionSort", pointList, Color.Blue, SymbolType.Default);
                                break;
                            case 3:
                                pane.AddCurve("SelectionSort", pointList, Color.Green, SymbolType.Default);
                                break;
                            case 4:
                                pane.AddCurve("ShakerSort", pointList, Color.Orchid, SymbolType.Default);
                                break;
                            case 5:
                                pane.AddCurve("GnomeSort", pointList, Color.Red, SymbolType.Default);
                                break;
                        }
                        break;
                    case 2:
                        switch (i)
                        {
                            case 0:
                                pane.AddCurve("BitonicSort", pointList, Color.Red, SymbolType.Default);
                                break;
                            case 2:
                                pane.AddCurve("ShellSort", pointList, Color.Yellow, SymbolType.Default);
                                break;
                            case 3:
                                pane.AddCurve("TreeSort", pointList, Color.Orange, SymbolType.Default);
                                break;
                        }
                        break;
                    case 3:
                        switch (i)
                        {
                            case 0:
                                pane.AddCurve("CombSort", pointList, Color.Red, SymbolType.Default);
                                break;
                            case 2:
                                pane.AddCurve("HeapSort", pointList, Color.Blue, SymbolType.Default);
                                break;
                            case 3:
                                pane.AddCurve("QuickSort", pointList, Color.Green, SymbolType.Default);
                                break;
                            case 4:
                                pane.AddCurve("MergeSort", pointList, Color.Yellow, SymbolType.Default);
                                break;
                            case 5:
                                pane.AddCurve("RadixSort", pointList, Color.Purple, SymbolType.Default);
                                break;
                        }
                        break;
                }
            }
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
