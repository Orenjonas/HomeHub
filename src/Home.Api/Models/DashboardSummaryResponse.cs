namespace Home.Api.Models;

public sealed record DashboardSummaryResponse(
    DateTimeOffset GeneratedAtUtc,
    WeatherSnapshot Weather,
    TransitSnapshot Transit,
    EnergySnapshot Energy);

public sealed record WeatherSnapshot(string Condition, decimal TemperatureC);

public sealed record TransitSnapshot(string Line, int MinutesUntilDeparture, string Status);

public sealed record EnergySnapshot(decimal CurrentPricePerKwh, string Currency, string Source);
