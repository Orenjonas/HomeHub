using Energy.Service.Interfaces;
using Home.Api.Models;
using Transit.Service.Interfaces;
using Weather.Service.Interfaces;

namespace Home.Api.Services;

public sealed class InMemoryDashboardSummaryService : IDashboardSummaryService
{
    private readonly IWeatherProvider weatherProvider;
    private readonly ITransitProvider transitProvider;
    private readonly IEnergyPriceProvider energyPriceProvider;

    public InMemoryDashboardSummaryService(IWeatherProvider weatherProvider, ITransitProvider transitProvider, IEnergyPriceProvider energyPriceProvider)
    {
        this.weatherProvider = weatherProvider;
        this.transitProvider = transitProvider;
        this.energyPriceProvider = energyPriceProvider;
    }

    public async Task<DashboardSummaryResponse> GetSummaryAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var weather = await weatherProvider.GetCurrentWeatherAsync(cancellationToken);
        var departure = await transitProvider.GetNextDepartureAsync(cancellationToken);
        var energy = await energyPriceProvider.GetCurrentPriceAsync(cancellationToken);

        var summary = new DashboardSummaryResponse(
            GeneratedAtUtc: DateTimeOffset.UtcNow,
            Weather: new WeatherSnapshot(Condition: weather.Condition, TemperatureC: weather.TemperatureC),
            Transit: new TransitSnapshot(Line: departure.Line, MinutesUntilDeparture: departure.MinutesUntilDeparture, Status: departure.Status),
            Energy: new EnergySnapshot(CurrentPricePerKwh: energy.PricePerKwh, Currency: energy.Currency, Source: energy.Source));

        return summary;
    }
}
