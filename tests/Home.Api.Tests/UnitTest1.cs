using System.Net;
using System.Net.Http.Json;
using Home.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Home.Api.Tests;

public sealed class DashboardSummaryEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public DashboardSummaryEndpointTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task GetSummary_ReturnsSeededSummaryPayload()
    {
        var response = await client.GetAsync("/api/dashboard/summary");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<DashboardSummaryResponse>();

        Assert.NotNull(payload);
        Assert.Equal("Partly cloudy", payload!.Weather.Condition);
        Assert.Equal("R10", payload.Transit.Line);
        Assert.Equal("seed", payload.Energy.Source);
        Assert.True(payload.GeneratedAtUtc <= DateTimeOffset.UtcNow);
    }
}
