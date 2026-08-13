using Transit.Service.Models;

namespace Transit.Service.Interfaces;

public interface ITransitProvider
{
    Task<NextDeparture> GetNextDepartureAsync(CancellationToken cancellationToken);
}
