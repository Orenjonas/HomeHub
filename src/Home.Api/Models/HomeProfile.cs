namespace Home.Api.Models;

public sealed class HomeProfile
{
    public Guid Id { get; set; }

    public required string DisplayName { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
}