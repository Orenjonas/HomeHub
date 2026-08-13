using Energy.Service.Interfaces;
using Energy.Service.Providers;
using Home.Api.Services;
using Transit.Service.Interfaces;
using Transit.Service.Providers;
using Weather.Service.Interfaces;
using Weather.Service.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IWeatherProvider, SeedWeatherProvider>();
builder.Services.AddSingleton<ITransitProvider, SeedTransitProvider>();
builder.Services.AddSingleton<IEnergyPriceProvider, SeedEnergyPriceProvider>();
builder.Services.AddSingleton<IDashboardSummaryService, InMemoryDashboardSummaryService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
