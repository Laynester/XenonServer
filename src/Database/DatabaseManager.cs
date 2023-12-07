using Dapper;
using Xenon.Database.Entities;
using Xenon.Utils;
using MySql.Data.MySqlClient;

namespace Xenon.Database;

public class DatabaseManager
{
    private Logger _Logger;
    private MySqlConnection connection;

    public DatabaseManager()
    {
        _Logger = new Logger(GetType().Name);

        try
        {
            Initialize();

            using (var connection = this.connection)
            {
                // Create a query that retrieves all authors"    
                var sql = "SELECT * FROM server_config";
                // Use the Query method to execute the query and return a list of objects
                List<ServerConfigEntity> authors = connection.Query<ServerConfigEntity>(sql).ToList();
            }
        }
        catch (System.Exception)
        {

            _Logger.Error("Could not connect to database!");
        }
    }

    private void Initialize()
    {
        var server = "localhost";  // Update with your MySQL server
        var database = "Xenon";  // Update with your database name
        var uid = "root";  // Update with your MySQL username
        var password = "";  // Update with your MySQL password

        string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

        connection = new MySqlConnection(connectionString);
        connection.Open();
    }
}