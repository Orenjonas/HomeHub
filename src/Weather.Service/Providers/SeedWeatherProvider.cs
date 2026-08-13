using Weather.Service.Interfaces;
using Weather.Service.Models;

namespace Weather.Service.Providers;

public sealed class SeedWeatherProvider : IWeatherProvider
{
    public Task<CurrentWeather> GetCurrentWeatherAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new CurrentWeather(
            Condition: "Partly cloudy",
            TemperatureC: 17.4m,
            Source: "seed"));
    }
}
