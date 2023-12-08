using System.Net;
using Xenon.Communication;
using Xenon.Core.Config;
using Xenon.Database;
using Xenon.Utils;

namespace Xenon;

public sealed class XenonEnvironment : Loggable
{

    private static DatabaseManager _database = null!;
    private static WebSockets _sockets = null!;
    private static ConfigManager _config = null!;

    public XenonEnvironment()
    {
        Logger.Logo();

        try
        {
            _database = new DatabaseManager();

            // Starting the socket server should be the last thing we do, we don't want clients connecting before we're ready
            _sockets = new WebSockets(IPAddress.Any, 3000);
            _sockets.Start();

            _config = new ConfigManager();
            _config.Load();

            _logger.Info("Successfully initialized Xenon!");

            ConsoleScanner();
        }
        catch (Exception ex)
        {
            if (ex.Message.Length > 0)
                _logger.Error(ex);
        }

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

    public static DatabaseManager Database() => _database;

    public static ConfigManager Config() => _config;

}
