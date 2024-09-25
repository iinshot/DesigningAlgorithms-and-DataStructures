using System.Collections;
using MyArrayLib;
public class Program
{
    static void Main(string[] args)
    {
        MyArrayList<string> uniqueTags = new MyArrayList<string>(10);
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                // find a symbol '<'
                if (line[i] == '<')
                {
                    // find a symbol '>'
                    int endIndex = line.IndexOf('>', i);
                    if (endIndex != -1)
                    {
                        // extraction tag
                        string tag = line.Substring(i, endIndex - i + 1);
                        string cleanTag = tag.Trim('<', '>').ToLower();
                        uniqueTags.AddElement(cleanTag);
                        i = endIndex;
                    }
                }
            }
        }
    }
}
