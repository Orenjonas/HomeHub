using Home.Api.Persistence;
using Home.Api.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Home.Api.Tests;

public sealed class EfHomeProfileServiceTests : IAsyncLifetime
{
    private SqliteConnection connection = null!;
    private HomeDbContext dbContext = null!;
    private EfHomeProfileService service = null!;

    public async Task InitializeAsync()
    {
        connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<HomeDbContext>()
            .UseSqlite(connection)
            .Options;
        dbContext = new HomeDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();
        service = new EfHomeProfileService(dbContext);
    }

    public async Task DisposeAsync()
    {
        await dbContext.DisposeAsync();
        await connection.DisposeAsync();
    }

    [Fact]
    public async Task CreateAsync_TrimsAndPersistsHomeProfile()
    {
        var home = await service.CreateAsync("  Main home  ", CancellationToken.None);

        Assert.NotEqual(Guid.Empty, home.Id);
        Assert.Equal("Main home", home.DisplayName);
        Assert.InRange(home.CreatedAtUtc, DateTimeOffset.UtcNow.AddSeconds(-5), DateTimeOffset.UtcNow);

        var persisted = await service.GetAsync(home.Id, CancellationToken.None);

        Assert.NotNull(persisted);
        Assert.Equal(home.Id, persisted!.Id);
        Assert.Equal("Main home", persisted.DisplayName);
    }

    [Fact]
    public async Task CreateAsync_WithBlankName_ThrowsArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateAsync("  ", CancellationToken.None));
    }

    [Fact]
    public async Task GetAsync_WhenHomeDoesNotExist_ReturnsNull()
    {
        var home = await service.GetAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.Null(home);
    }

    [Fact]
    public async Task CreateAsync_WhenCancellationRequested_ThrowsBeforeWriting()
    {
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.CreateAsync("Main home", cancellationSource.Token));

        Assert.Empty(await dbContext.HomeProfiles.ToListAsync());
    }
}