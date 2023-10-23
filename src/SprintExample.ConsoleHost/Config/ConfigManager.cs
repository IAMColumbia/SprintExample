namespace SprintExample.ConsoleHost.Config;

public interface IConfigManager
{
    public string GetValue(Func<ConfigData, string> selector);
    public void Load();
}

public class InMemoryConfigManager : IConfigManager
{
    private ConfigData? config;
    public InMemoryConfigManager()
    {
        this.config = new ConfigData();
    }
    public string GetValue(Func<ConfigData, string> selector)
    {
        if(this.config == null)
        {
            throw new Exception("Config not loaded");
        }
        return selector(this.config);
    }

    public void Load()
    {
        if(this.config == null)
        {
            throw new Exception("Config not loaded");
        }

        if(this.config.OpenWeather == null)
        {
            throw new Exception("OpenWeather config not loaded");
        }
        this.config.OpenWeather.ApiKey = "abc123";
    }
}


public class FileConfigManager : IConfigManager
{
    private const string DefaultConfigPath = "$HOME/.config/sprint-example/config.json";
    private ConfigData? config;
    public FileConfigManager()
    {
        var configPath = Environment.GetEnvironmentVariable("CONFIG_PATH") ?? DefaultConfigPath;
        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Config file not found at {configPath}");
        }
    }
    public string GetValue(Func<ConfigData, string> selector)
    {
        if(this.config == null)
        {
            throw new Exception("Config not loaded");
        }
        return selector(this.config);
    }

    public void Load()
    {
        var stringText = File.ReadAllText(DefaultConfigPath);

        if (string.IsNullOrWhiteSpace(stringText))
        {
            throw new Exception("Config file is empty");
        }
        this.config = System.Text.Json.JsonSerializer.Deserialize<Config>(stringText);
    }


}

public class OpenWeather
{
    public string? ApiKey { get; set; }
}

public class ConfigData
{
    public OpenWeather? OpenWeather { get; set; }
}
