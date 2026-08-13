using Weather.Service.Providers;

namespace Weather.Service.Tests;

public sealed class SeedWeatherProviderTests
{
    [Fact]
    public async Task GetCurrentWeatherAsync_ReturnsSeededWeather()
    {
        var provider = new SeedWeatherProvider();

        var weather = await provider.GetCurrentWeatherAsync(CancellationToken.None);

        Assert.Equal("Partly cloudy", weather.Condition);
        Assert.Equal(17.4m, weather.TemperatureC);
        Assert.Equal("seed", weather.Source);
    }
}
