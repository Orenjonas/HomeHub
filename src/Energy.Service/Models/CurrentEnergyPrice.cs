namespace Energy.Service.Models;

public sealed record CurrentEnergyPrice(
    decimal PricePerKwh,
    string Currency,
    string Source);
