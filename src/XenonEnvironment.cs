using System.Net;
using System.Net.Sockets;
using Xenon.Communication;
using Xenon.Database;
using Xenon.Utils;

namespace Xenon;

public sealed class XenonEnvironment
{
    
    private Logger _Logger;
    private Websockets? _Sockets;
    private DatabaseManager _DatabaseManager;
    
    private XenonEnvironment()
    {
        _Logger = new Logger(GetType().Name);

        Logger.Logo();

        Initialize();
    }

    private void Initialize()
    {
        _Sockets = new Websockets(IPAddress.Any, 3000);
        _Sockets.Start();

        _DatabaseManager = new DatabaseManager();

        ConsoleScanner();
    }

    private void ConsoleScanner()
    {
        for (; ; )
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                break;

            // Restart the server
            if (line == "!")
            {
                Console.Write("Server restarting...");
                _Sockets.Restart();
                Console.WriteLine("Done!");
            }

            // Multicast admin message to all sessions
            line = "(admin) " + line;
            _Sockets.MulticastText(line);
        }
    }
    
    public static XenonEnvironment Instance { get; } = new();
    
}
