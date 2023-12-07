using System.Net;
using System.Net.Sockets;
using Xenon.Communication;
using Xenon.Database;
using Xenon.Utils;

namespace Xenon;

public sealed class XenonEnvironment
{
    private static readonly XenonEnvironment instance = new XenonEnvironment();
    private Logger _Logger;
    private Websockets? _Sockets;
    private DatabaseManager _DatabaseManager;

    static XenonEnvironment() { }
    private XenonEnvironment()
    {
        _Logger = new Logger(GetType().Name);

        _Logger.Logo();

        Initialize();
    }

    public void Initialize()
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
            string line = Console.ReadLine();
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


    public static XenonEnvironment Instance
    {
        get
        {
            return instance;
        }
    }
}
