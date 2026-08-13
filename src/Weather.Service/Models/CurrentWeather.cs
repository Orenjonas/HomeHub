namespace Weather.Service.Models;

public sealed record CurrentWeather(
    string Condition,
    decimal TemperatureC,
    string Source);
