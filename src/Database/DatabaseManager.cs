using Dapper;
using Xenon.Database.Entities;
using MySqlConnector;
using Xenon.Utils;

namespace Xenon.Database;

public class DatabaseManager : Loggable
{

    private readonly string _connectionStr;

    public List<ServerConfigEntity> ServerConfig { get; init; }

    public DatabaseManager()
    {
        _logger.Info("Initializing Database");

        try
        {
            var server = "localhost";
            var database = "Xenon";
            var uid = "root";
            var password = "";

            _connectionStr = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            _logger.Info("Database initialized!");
        }
        catch (Exception)
        {
            _logger.Error("Database failed to connect!");
            throw new Exception("");
        }
    }

    public MySqlConnection Connection()
    {
        return new MySqlConnection(_connectionStr);
    }

}