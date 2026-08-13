using Energy.Service.Interfaces;
using Energy.Service.Models;

namespace Energy.Service.Providers;

public sealed class SeedEnergyPriceProvider : IEnergyPriceProvider
{
    public Task<CurrentEnergyPrice> GetCurrentPriceAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new CurrentEnergyPrice(
            PricePerKwh: 0.92m,
            Currency: "NOK",
            Source: "seed"));
    }
}
