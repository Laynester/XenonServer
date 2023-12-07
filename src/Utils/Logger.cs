using System;
using System;
using System.IO;

namespace Xenon.Utils;

public class Logger
{
    private static long LAST_TIMESTAMP = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    private string _name;
    private object _description;
    private bool _print;
    private int maxChar = 20;
    private Func<string, string> colour;

    public Logger(string name, object? description = null, Func<string, string>? colour = null)
    {
        this._name = name;
        this._description = description;
        this._print = true;
        this.colour = colour ?? Cyan;
    }

    public void Info(object message)
    {
        PrintMessage(message, Green);
    }

    public void Error(object message, object? trace = null)
    {
        PrintMessage(trace ?? message, Red);
    }

    public void Warn(object message)
    {
        PrintMessage(message, Yellow);
    }

    public void Debug(object message)
    {
        var temp = _description;
        _description = "DEBUG";
        PrintMessage($"{message}", YellowBright);
        _description = temp;
    }

    private void PrintMessage(object message, Func<string, string>? color = null)
    {
        if (!_print) return;

        var time = DateTimeOffset.Now.ToString("H:mm:ss");
        var name = $" [{_name}] ";

        Console.Write(time);

        var length = time.Length + (!string.IsNullOrEmpty(_name) ? name.Length : 0);

        for (var i = 0; i < maxChar - length; i++)
        {
            Console.Write(" ");
        }

        if (_name != null) Console.Write(colour(name));

        if (_description != null)
            Console.Write(Grey($"[{_description}] "));

        Console.Write(color?.Invoke(message.ToString()));

        PrintTimestamp();

        Console.Write("\n");
    }

    private void PrintTimestamp()
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        Console.Write(Grey($" +{now - LAST_TIMESTAMP}ms"));

        LAST_TIMESTAMP = now;
    }

    public void Logo()
    {
        Console.WriteLine(Cyan(@"
__  __                      
\ \/ /___ _ __   ___  _ __  
 \  // _ \ '_ \ / _ \| '_ \ 
 /  \  __/ | | | (_) | | | |
/_/\_\___|_| |_|\___/|_| |_|                          
"));
    }

    public object Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public bool Print
    {
        get { return _print; }
        set { _print = value; }
    }

    private static string Grey(string input) => $"\u001b[90m{input}\u001b[39m";
    private static string Green(string input) => $"\u001b[32m{input}\u001b[39m";
    private static string Red(string input) => $"\u001b[31m{input}\u001b[39m";
    private static string Yellow(string input) => $"\u001b[33m{input}\u001b[39m";
    private static string YellowBright(string input) => $"\u001b[93m{input}\u001b[39m";
    private static string Cyan(string input) => $"\u001b[36m{input}\u001b[39m";
}

