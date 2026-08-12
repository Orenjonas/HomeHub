using Home.Api.Models;

namespace Home.Api.Services;

public sealed class InMemoryDashboardSummaryService : IDashboardSummaryService
{
    public Task<DashboardSummaryResponse> GetSummaryAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var summary = new DashboardSummaryResponse(
            GeneratedAtUtc: DateTimeOffset.UtcNow,
            Weather: new WeatherSnapshot(Condition: "Partly cloudy", TemperatureC: 17.4m),
            Transit: new TransitSnapshot(Line: "R10", MinutesUntilDeparture: 6, Status: "On time"),
            Energy: new EnergySnapshot(CurrentPricePerKwh: 0.92m, Currency: "NOK", Source: "seed"));

        return Task.FromResult(summary);
    }
}
