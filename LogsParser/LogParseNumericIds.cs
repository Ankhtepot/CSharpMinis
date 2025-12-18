using System.Text.RegularExpressions;

namespace LogsParser;

public partial class LogParseNumericIds : ILogParseMethod
{
    public void SetData(string input, out Dictionary<string, int> results)
    {
        results = new Dictionary<string, int>();
        Regex regex = NumericIdRegex();
        MatchCollection matches = regex.Matches(input);

        foreach (Match match in matches)
        {
            string id = match.Groups[1].Value;
            results[id] = results.TryGetValue(id, out int value) ? value + 1 : 1;
        }
    }

    public void WriteResult(Dictionary<string, int> counts)
    {
        List<int> list = counts.Values.ToList();
        list.Sort();

        Console.WriteLine("Parsed Numeric IDs:");
        
        int count = 1;
        foreach (int value in list)
        {
            string id = counts.First(kvp => kvp.Value == value).Key;
            Console.WriteLine($"Id:{count} -> {id}{(value > 1 ? $":{value}" : string.Empty)}");
            counts.Remove(id);
            count++;
        }
    }
    
    [GeneratedRegex("shareLinkId=([0-9]+)", RegexOptions.Compiled)]
    private static partial Regex NumericIdRegex();
}