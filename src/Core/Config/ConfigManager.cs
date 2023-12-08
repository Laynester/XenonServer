using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;
using Xenon.Database.Entities;
using Xenon.Utils;

namespace Xenon.Core.Config;

public class ConfigManager : Loggable
{

    private Dictionary<string, string> ServerConfig = new(0);
    private Dictionary<string, string> ClientConfig = new(0);

    public ConfigManager()
    {

    }

    public void Load()
    {
        try
        {
            LoadServer();
            LoadClient();
        }
        catch (System.Exception)
        {
            _logger.Error("Config Failed to load!");
            throw;
        }
    }

    private void LoadServer()
    {
        using var connection = XenonEnvironment.Database().Connection();

        const string sql = "SELECT * FROM server_config";

        ServerConfig = connection.Query<ServerConfigEntity>(sql).ToDictionary(x => x.Key, x => x.Value);
    }

    private void LoadClient()
    {
        using var connection = XenonEnvironment.Database().Connection();

        const string sql = "SELECT * FROM client_config";

        ClientConfig = connection.Query<ClientConfigEntity>(sql).ToDictionary(x => x.Key, x => x.Value);
    }

    public string TryGetValue(string value) => ServerConfig.ContainsKey(value) ? ServerConfig[value] : $"No language locale found for [{value}]";

    public Dictionary<string, string> GetClienConfig() => ClientConfig;
}