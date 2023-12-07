using System.Diagnostics;
using System.Net;
using Xenon.Communication;
using Xenon.Database;
using Xenon.Utils;

namespace Xenon;

public sealed class XenonEnvironment
{
    
    private readonly Logger _logger = new(nameof(XenonEnvironment));
    
    private readonly WebSockets _sockets;
    private DatabaseManager _databaseManager = null!;
    
    private XenonEnvironment()
    {
        Logger.Logo();
        
        MeasureTimeTaken("Database", () => _databaseManager = new DatabaseManager());
        
        // Starting the socket server should be the last thing we do, we don't want clients connecting before we're ready
        _sockets = new WebSockets(IPAddress.Any, 3000);
        _sockets.Start();
        
        _logger.Info("Successfully initialized Xenon!");

        ConsoleScanner();
    }

    private void MeasureTimeTaken(string taskName, Action action)
    {
        _logger.Info($"Initializing {taskName}...");
        
        var stopwatch = new Stopwatch();
        
        stopwatch.Start();

        try
        {
            action();
        }
        catch (Exception exc)
        {
            _logger.Error($"Failed to initialize {taskName}!", exc);
            Environment.Exit(1);
        }
        
        stopwatch.Stop();
        
        _logger.Info($"Successfully initialized {taskName} in {stopwatch.ElapsedMilliseconds}ms!");
    }

    private void ConsoleScanner()
    {
        while (true)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                break;

            // Restart the server
            if (line == "!")
            {
                Console.Write("Server restarting...");
                _sockets.Restart();
                Console.WriteLine("Done!");
            }

            // Multicast admin message to all sessions
            line = "(admin) " + line;
            _sockets.MulticastText(line);
        }
    }
    
    public static XenonEnvironment Instance { get; } = new();
    
}
