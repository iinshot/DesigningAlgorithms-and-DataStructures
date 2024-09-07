string path = "TextFile1.txt";
string? line;
int n = 0;
int[][]? matrixG = null;
int[]? vectorX = null;
double LenghtVector(int[][] matrixG, int[] vectorX, int n)
{
    int sum;
    int[] new_matrixG = new int[n];
    for (int i = 0; i < n; i++)
    {
        sum = 0;
        for (int j = 0; j < n; j++)
        {
            sum += vectorX[j] * matrixG[j][i];
        }
        new_matrixG[i] = sum;
    }
    sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += new_matrixG[i] * vectorX[i];
    }
    return Math.Sqrt(sum);
}
bool CheckSymmetry(int[][] matrixG, int n)
{
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (matrixG[i][j] != matrixG[j][i]) return false;
        }
    return true;
}
try
{
    StreamReader sr = new StreamReader(path);
    line = sr.ReadLine();
    n = Convert.ToInt32(line);
    matrixG = new int[n][];

    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        if (line != null) matrixG[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    }

    Console.WriteLine($"Ваша матрица размера {n}:");
    for (int i = 0; i < n; i++)
    {
        foreach (int b in matrixG[i]) Console.Write(b + " ");
        Console.WriteLine();
    }

    line = sr.ReadLine();
    if (line != null) vectorX = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    sr.Close();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

if (vectorX != null)
{
    Console.WriteLine($"Ваш вектор размера {n}:");
    foreach (int b in vectorX) Console.Write(b + " ");
    Console.WriteLine();
}

else
{
    Console.WriteLine("Вектор не был инициализирован.");
    return;
}

if (matrixG != null)
{
    if (CheckSymmetry(matrixG, n)) Console.WriteLine($"Произведение: {LenghtVector(matrixG, vectorX, n)}");
    else Console.WriteLine("Матрица несимметрична.");
}

else Console.WriteLine("Матрица не была инициализирована.");