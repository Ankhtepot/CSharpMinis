namespace LogsParser;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        using HttpClient client = new();
        string logString = await client.GetStringAsync("https://coderbyte.com/api/challenges/logs/web-logs-raw");

        ArgumentsOption options = ProcessArguments(args);

        options.ParseMethod.SetData(logString, out Dictionary<string, int> counts);
        options.ParseMethod.WriteResult(counts);
    }

    private static ArgumentsOption ProcessArguments(string[]? args = null)
    {
        if (args == null || args.Length == 0)
        {
            return new ArgumentsOption
            {
                ParseMethod = new LogParseIds()
            };
        }

        foreach (string argument in args)
        {
            switch (argument)
            {
                case "--help":
                case "-h":
                    Console.WriteLine("Usage: LogsParser [--numeric | --alphanumeric]");
                    Console.WriteLine("--numeric       Use numeric IDs only.");
                    Console.WriteLine("--alphanumeric  Use alphanumeric IDs (default).");
                    Environment.Exit(0);
                    break;
                case "--numeric":
                    return new ArgumentsOption()
                    {
                        ParseMethod = new LogParseNumericIds()
                    };
                case "--alphanumeric":
                    return new ArgumentsOption()
                    {
                        ParseMethod = new LogParseIds()
                    };
            }
        }
        
        return new ArgumentsOption
        {
            ParseMethod = new LogParseIds()
        };
    }
}