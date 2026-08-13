using Transit.Service.Providers;

namespace Transit.Service.Tests;

public sealed class SeedTransitProviderTests
{
    [Fact]
    public async Task GetNextDepartureAsync_ReturnsSeededTransitData()
    {
        var provider = new SeedTransitProvider();

        var departure = await provider.GetNextDepartureAsync(CancellationToken.None);

        Assert.Equal("R10", departure.Line);
        Assert.Equal(6, departure.MinutesUntilDeparture);
        Assert.Equal("On time", departure.Status);
        Assert.Equal("seed", departure.Source);
    }
}
