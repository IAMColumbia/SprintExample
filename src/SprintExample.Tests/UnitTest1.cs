using SprintExample.ConsoleHost.Config;

namespace SprintExample.Tests;

public class ConfigTests
{
    [Fact]
    public void DoesConfigValueLoad()
    {
        var configManager = new InMemoryConfigManager();
        configManager.Load();
        var apiKey = configManager.GetValue(x => x.OpenWeather.ApiKey);
        Assert.Equal("abc123", apiKey);
    }
}
