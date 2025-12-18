using System.Text.RegularExpressions;

namespace LogsParser;

public interface ILogParseMethod
{
    public void SetData(string input, out Dictionary<string, int> results);
    public void WriteResult(Dictionary<string, int> counts);
}