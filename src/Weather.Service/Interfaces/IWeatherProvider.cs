using Weather.Service.Models;

namespace Weather.Service.Interfaces;

public interface IWeatherProvider
{
    Task<CurrentWeather> GetCurrentWeatherAsync(CancellationToken cancellationToken);
}
