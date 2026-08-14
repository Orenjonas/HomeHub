using Home.Api.Models;

namespace Home.Api.Services;

public interface IHomeProfileService
{
    Task<HomeProfile> CreateAsync(string displayName, CancellationToken cancellationToken);

    Task<HomeProfile?> GetAsync(Guid id, CancellationToken cancellationToken);
}