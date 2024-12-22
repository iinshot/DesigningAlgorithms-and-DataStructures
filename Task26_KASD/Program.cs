using HashSet;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            MatchCollection matches = Regex.Matches(line, "[a-zA-Z]+$");
            foreach(Match match in matches)
                hashSet.Add(match.Value.ToLower());
            line = sr.ReadLine();
        }
        Console.WriteLine("Unique words:");
        string[] words = hashSet.ToArray();
        foreach(string word in words)
            Console.WriteLine(word);
    }
}