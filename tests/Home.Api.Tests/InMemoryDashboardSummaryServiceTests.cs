using Energy.Service.Interfaces;
using Energy.Service.Models;
using Home.Api.Services;
using Transit.Service.Interfaces;
using Transit.Service.Models;
using Weather.Service.Interfaces;
using Weather.Service.Models;

namespace Home.Api.Tests;

public sealed class InMemoryDashboardSummaryServiceTests
{
    [Fact]
    public async Task GetSummaryAsync_MapsProviderDataToDashboardSummary()
    {
        var weatherProvider = new StubWeatherProvider(
            new CurrentWeather("Clear", 21.5m, "weather-test"));
        var transitProvider = new StubTransitProvider(
            new NextDeparture("T4", 12, "Delayed", "transit-test"));
        var energyProvider = new StubEnergyPriceProvider(
            new CurrentEnergyPrice(1.17m, "NOK", "energy-test"));
        var service = new InMemoryDashboardSummaryService(
            weatherProvider,
            transitProvider,
            energyProvider);

        var before = DateTimeOffset.UtcNow;
        var summary = await service.GetSummaryAsync(CancellationToken.None);
        var after = DateTimeOffset.UtcNow;

        Assert.Equal("Clear", summary.Weather.Condition);
        Assert.Equal(21.5m, summary.Weather.TemperatureC);
        Assert.Equal("T4", summary.Transit.Line);
        Assert.Equal(12, summary.Transit.MinutesUntilDeparture);
        Assert.Equal("Delayed", summary.Transit.Status);
        Assert.Equal(1.17m, summary.Energy.CurrentPricePerKwh);
        Assert.Equal("NOK", summary.Energy.Currency);
        Assert.Equal("energy-test", summary.Energy.Source);
        Assert.InRange(summary.GeneratedAtUtc, before, after);
    }

    [Fact]
    public async Task GetSummaryAsync_WhenCancellationRequested_ThrowsBeforeCallingProviders()
    {
        var weatherProvider = new StubWeatherProvider(
            new CurrentWeather("Clear", 21.5m, "weather-test"));
        var transitProvider = new StubTransitProvider(
            new NextDeparture("T4", 12, "On time", "transit-test"));
        var energyProvider = new StubEnergyPriceProvider(
            new CurrentEnergyPrice(1.17m, "NOK", "energy-test"));
        var service = new InMemoryDashboardSummaryService(
            weatherProvider,
            transitProvider,
            energyProvider);
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            service.GetSummaryAsync(cancellationSource.Token));

        Assert.False(weatherProvider.WasCalled);
        Assert.False(transitProvider.WasCalled);
        Assert.False(energyProvider.WasCalled);
    }

    private sealed class StubWeatherProvider(CurrentWeather result) : IWeatherProvider
    {
        public bool WasCalled { get; private set; }

        public Task<CurrentWeather> GetCurrentWeatherAsync(CancellationToken cancellationToken)
        {
            WasCalled = true;
            return Task.FromResult(result);
        }
    }

    private sealed class StubTransitProvider(NextDeparture result) : ITransitProvider
    {
        public bool WasCalled { get; private set; }

        public Task<NextDeparture> GetNextDepartureAsync(CancellationToken cancellationToken)
        {
            WasCalled = true;
            return Task.FromResult(result);
        }
    }

    private sealed class StubEnergyPriceProvider(CurrentEnergyPrice result) : IEnergyPriceProvider
    {
        public bool WasCalled { get; private set; }

        public Task<CurrentEnergyPrice> GetCurrentPriceAsync(CancellationToken cancellationToken)
        {
            WasCalled = true;
            return Task.FromResult(result);
        }
    }
}