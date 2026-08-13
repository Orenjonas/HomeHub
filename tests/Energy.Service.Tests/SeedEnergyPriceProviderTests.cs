using Energy.Service.Providers;

namespace Energy.Service.Tests;

public sealed class SeedEnergyPriceProviderTests
{
    [Fact]
    public async Task GetCurrentPriceAsync_ReturnsSeededEnergyData()
    {
        var provider = new SeedEnergyPriceProvider();

        var price = await provider.GetCurrentPriceAsync(CancellationToken.None);

        Assert.Equal(0.92m, price.PricePerKwh);
        Assert.Equal("NOK", price.Currency);
        Assert.Equal("seed", price.Source);
    }
}
