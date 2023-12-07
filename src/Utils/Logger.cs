namespace Xenon.Utils;

public class Logger
{
    
    private static long _lastTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    private readonly string? _name;
    private readonly bool _print;
    private readonly Func<string, string> _colour;
    
    private const int MaxChar = 20;

    public Logger(string name, object? description = null, Func<string, string>? colour = null)
    {
        _name = name;
        Description = description;
        _print = true;
        _colour = colour ?? Cyan;
    }

    public void Info(object message)
    {
        PrintMessage(message, Green);
    }

    public void Error(object message)
    {
        PrintMessage(message, Red);
    }
    
    public void Error<T>(object message, T? trace = null) where T : Exception?
    {
        PrintMessage(message, Red);
        
        if (trace != null) 
            PrintMessage(trace, Red);
    }

    public void Warn(object message)
    {
        PrintMessage(message, Yellow);
    }

    public void Debug(object message)
    {
        var temp = Description;
        Description = "DEBUG";
        PrintMessage($"{message}", YellowBright);
        Description = temp;
    }

    private void PrintMessage(object message, Func<string, string>? color = null)
    {
        if (!_print) return;

        var time = DateTimeOffset.Now.ToString("H:mm:ss.fff");
        var name = $" [{_name}] ";

        Console.Write(time);

        var length = time.Length + (!string.IsNullOrEmpty(_name) ? name.Length : 0);

        for (var i = 0; i < MaxChar - length; i++)
        {
            Console.Write(" ");
        }

        if (_name != null) Console.Write(_colour(name));

        if (Description != null)
            Console.Write(Grey($"[{Description}] "));

        var stringMessage = message.ToString();
        if (stringMessage != null)
            Console.Write(color?.Invoke(stringMessage));

        PrintTimestamp();

        Console.Write("\n");
    }

    private static void PrintTimestamp()
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        Console.Write(Grey($" +{now - _lastTimestamp}ms"));

        _lastTimestamp = now;
    }

    public static void Logo()
    {
        Console.WriteLine(Cyan("""

                               __  __
                               \ \/ /___ _ __   ___  _ __
                                \  // _ \ '_ \ / _ \| '_ \
                                /  \  __/ | | | (_) | | | |
                               /_/\_\___|_| |_|\___/|_| |_|

                               """));
    }

    public object? Description { get; set; }

    public bool Print { get; set; }

    private static string Grey(string input) => $"\u001b[90m{input}\u001b[39m";
    private static string Green(string input) => $"\u001b[32m{input}\u001b[39m";
    private static string Red(string input) => $"\u001b[31m{input}\u001b[39m";
    private static string Yellow(string input) => $"\u001b[33m{input}\u001b[39m";
    private static string YellowBright(string input) => $"\u001b[93m{input}\u001b[39m";
    private static string Cyan(string input) => $"\u001b[36m{input}\u001b[39m";
    
}