string path = "TextFile1.txt";
string? line;
int n;
double Product(int[][] G, int[] vector, int n)
{
    int sum;
    int[] NewG = new int[n]; 
    for (int i = 0; i < n; i++)
    {
        sum = 0;
        for (int j = 0; j < n; j++)
        {
            sum += vector[j] * G[j][i];
        }
        NewG[i] = sum;
    }
    sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += NewG[i] * vector[i];
    }
    return Math.Sqrt(sum);
}
bool CheckSymmetry(int[][] G, int n)
{
    bool check = true;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (G[i][j] == G[j][i]) check = true;
            else check = false;
        }
    if (check == true) return true;
    else return false;
}
try
{
    StreamReader sr = new StreamReader(path);
    line = sr.ReadLine();
    n = Convert.ToInt32(line);
    int[][] G = new int[n][];
    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        G[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    }
    Console.WriteLine($"Ваша матрица размера {n}:");
    for (int i = 0; i < n; i++)
    {
        foreach(int b in G[i]) Console.Write(b + " ");
        Console.WriteLine();
    }
    line = sr.ReadLine();
    int[] vector = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    sr.Close();
    Console.WriteLine($"Ваш вектор размера {n}:");
    foreach (int b in vector) Console.Write(b + " ");
    Console.WriteLine();
    if (CheckSymmetry(G, n)) Console.WriteLine($"Произведение: {Product(G, vector, n)}");
    else Console.WriteLine("Матрица несимметрична");
} 
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
