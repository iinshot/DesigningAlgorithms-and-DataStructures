using HashSet;
public class Program
{
    static void Main(string[] args)
    {
        MyHashSet<string> hashSet = new MyHashSet<string>();
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string line = "";
        sr.ReadLine();
        while (line != null)
        {
            string word = "";
            bool fl = false;
            foreach(char symbol in line)
            {
                if (Char.IsLetter(symbol))
                {
                    word += symbol.ToString().ToLower();
                    fl = true;
                }
                else
                    if (fl)
                {
                    hashSet.Add(word);
                    word = "";
                    fl = false;
                }
            }
            line = sr.ReadLine();
        }
        string[] words = hashSet.ToArray();
        foreach(string s in words)
            Console.WriteLine(s);
    }
}