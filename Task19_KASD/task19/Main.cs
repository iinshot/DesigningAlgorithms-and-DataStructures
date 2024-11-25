using HashMap;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
public class Tegs
{
    static void Main(string[] args)
    {
        MyHashMap<string, int> tegs = new MyHashMap<string, int>();
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();
        string pattern = @"(?<=</?)?\w+ ?(?=/?>)";
        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                if (!tegs.ContainsKey(match.Value.ToLower()))
                    tegs.Put(match.Value.ToLower(), 1);
                else
                    tegs.Put(match.Value.ToLower(), tegs.Get(match.Value.ToLower()) + 1);
            }
            line = sr.ReadLine();
        }
        sr.Close();
        IEnumerable<KeyValuePair<string, int>> pairs = tegs.EntrySet();
        foreach(var pair in pairs)
            Console.WriteLine($"{pair.Key} : {pair.Value}");
    }
}