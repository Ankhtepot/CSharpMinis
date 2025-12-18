namespace LogsParser;

public class ArgumentsOption
{
    public ILogParseMethod ParseMethod;

    private ArgumentsOption()
    {
        ParseMethod = new LogParseIds();
    }

    public static ArgumentsOption Create(string[] args)
    {
        return ProcessArguments(args);
    }
    
    private static ArgumentsOption ProcessArguments(string[]? args = null)
    {
        ArgumentsOption options = new();
        
        if (args == null || args.Length == 0)
        {
            options.ParseMethod = new LogParseIds();
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
                    options.ParseMethod = new LogParseNumericIds();
                    break;
                case "--alphanumeric":
                    options.ParseMethod = new LogParseIds();
                    break;
            }
        }
        
        return options;
    }
}