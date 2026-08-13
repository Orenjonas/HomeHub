using Transit.Service.Interfaces;
using Transit.Service.Models;

namespace Transit.Service.Providers;

public sealed class SeedTransitProvider : ITransitProvider
{
    public Task<NextDeparture> GetNextDepartureAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new NextDeparture(
            Line: "R10",
            MinutesUntilDeparture: 6,
            Status: "On time",
            Source: "seed"));
    }
}
