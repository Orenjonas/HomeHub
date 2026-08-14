using Home.Api.Models;
using Home.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Home.Api.Services;

public sealed class EfHomeProfileService(HomeDbContext dbContext) : IHomeProfileService
{
    public async Task<HomeProfile> CreateAsync(
        string displayName,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        var home = new HomeProfile
        {
            Id = Guid.NewGuid(),
            DisplayName = displayName.Trim(),
            CreatedAtUtc = DateTimeOffset.UtcNow
        };

        dbContext.HomeProfiles.Add(home);
        await dbContext.SaveChangesAsync(cancellationToken);

        return home;
    }

    public Task<HomeProfile?> GetAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.HomeProfiles
            .AsNoTracking()
            .SingleOrDefaultAsync(home => home.Id == id, cancellationToken);
}