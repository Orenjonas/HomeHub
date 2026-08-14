using System.Net;
using System.Net.Http.Json;
using Home.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Security.Claims;

namespace Home.Api.Tests;

public sealed class DashboardSummaryEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public DashboardSummaryEndpointTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuthenticationHandler.Scheme;
                        options.DefaultChallengeScheme = TestAuthenticationHandler.Scheme;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                        TestAuthenticationHandler.Scheme,
                        _ => { });
            });
        }).CreateClient();
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

    [Fact]
    public async Task GetSummary_ReturnsCompleteSeededDashboardPayload()
    {
        var response = await client.GetAsync("/api/dashboard/summary");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/json", response.Content.Headers.ContentType?.MediaType);

        var payload = await response.Content.ReadFromJsonAsync<DashboardSummaryResponse>();

        Assert.NotNull(payload);
        Assert.Equal("Partly cloudy", payload!.Weather.Condition);
        Assert.Equal(17.4m, payload.Weather.TemperatureC);
        Assert.Equal("R10", payload.Transit.Line);
        Assert.Equal(6, payload.Transit.MinutesUntilDeparture);
        Assert.Equal("On time", payload.Transit.Status);
        Assert.Equal(0.92m, payload.Energy.CurrentPricePerKwh);
        Assert.Equal("NOK", payload.Energy.Currency);
        Assert.Equal("seed", payload.Energy.Source);
    }

    [Fact]
    public async Task GetSummary_WithoutCredentials_ReturnsUnauthorized()
    {
        using var unauthenticatedClient = factory.CreateClient();

        var response = await unauthenticatedClient.GetAsync("/api/dashboard/summary");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}

internal sealed class TestAuthenticationHandler(
    Microsoft.Extensions.Options.IOptionsMonitor<AuthenticationSchemeOptions> options,
    Microsoft.Extensions.Logging.ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public new const string Scheme = "Test";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(
            [new Claim(ClaimTypes.NameIdentifier, "local-test-user")],
            Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
