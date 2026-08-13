using Energy.Service.Models;

namespace Energy.Service.Interfaces;

public interface IEnergyPriceProvider
{
    Task<CurrentEnergyPrice> GetCurrentPriceAsync(CancellationToken cancellationToken);
}
