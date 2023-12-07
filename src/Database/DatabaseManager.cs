using Dapper;
using Xenon.Database.Entities;
using MySql.Data.MySqlClient;

namespace Xenon.Database;

public class DatabaseManager
{
    
    private readonly MySqlConnection _connection;
    
    public List<ServerConfigEntity> ServerConfig { get; init; }

    public DatabaseManager()
    {
        var server = "localhost";
        var database = "Xenon";
        var uid = "root";
        var password = "";

        var connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

        _connection = new MySqlConnection(connectionString);
        _connection.Open();

        using var connection = _connection;
            
        // Create a query that retrieves all authors"    
        const string sql = "SELECT * FROM server_config";
            
        // Use the Query method to execute the query and return a list of objects
        ServerConfig = connection.Query<ServerConfigEntity>(sql).ToList();
    }
    
}