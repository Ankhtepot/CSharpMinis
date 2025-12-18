namespace LogsParser;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        using HttpClient client = new();
        string logString = await client.GetStringAsync("https://coderbyte.com/api/challenges/logs/web-logs-raw");

        ArgumentsOption options = ArgumentsOption.Create(args);

        options.ParseMethod.SetData(logString, out Dictionary<string, int> counts);
        options.ParseMethod.WriteResult(counts);
    }
}