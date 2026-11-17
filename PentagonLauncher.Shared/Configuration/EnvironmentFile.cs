namespace PentagonLauncher.Shared.Configuration;

public static class EnvironmentFile
{
    public static void Load()
    {
        string? directory = AppContext.BaseDirectory;
        while (!string.IsNullOrEmpty(directory))
        {
            string envPath = Path.Combine(directory, ".env");
            if (File.Exists(envPath))
            {
                foreach (string rawLine in File.ReadLines(envPath))
                {
                    string line = rawLine.Trim();
                    if (line.Length == 0 || line.StartsWith('#')) continue;

                    int separator = line.IndexOf('=');
                    if (separator <= 0) continue;

                    string key = line[..separator].Trim();
                    string value = line[(separator + 1)..].Trim().Trim('"');
                    if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
                        Environment.SetEnvironmentVariable(key, value);
                }
                return;
            }

            directory = Directory.GetParent(directory)?.FullName;
        }
    }

    public static int GetPort(string key, int defaultValue)
    {
        string? value = Environment.GetEnvironmentVariable(key);
        return int.TryParse(value, out int port) && port is > 0 and <= 65535
            ? port
            : defaultValue;
    }
}
