using MySqlConnector;

namespace PentagonLauncher.Backend.Configuration;

internal static class EnvironmentSettings
{
    public static int GetPort(string key, int defaultValue)
    {
        string? value = Environment.GetEnvironmentVariable(key);
        return int.TryParse(value, out int port) && port is > 0 and <= 65535
            ? port
            : defaultValue;
    }

    public static string GetDatabaseConnectionString()
    {
        MySqlConnectionStringBuilder builder = new()
        {
            Server = GetValue("DB_HOST", "localhost"),
            Port = (uint)GetPort("DB_PORT", 3306),
            Database = GetValue("DB_NAME", "user_management"),
            UserID = GetValue("DB_USER", "root"),
            Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? string.Empty
        };

        return builder.ConnectionString;
    }

    private static string GetValue(string key, string defaultValue) =>
        string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key))
            ? defaultValue
            : Environment.GetEnvironmentVariable(key)!;
}
