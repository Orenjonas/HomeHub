namespace Transit.Service.Models;

public sealed record NextDeparture(
    string Line,
    int MinutesUntilDeparture,
    string Status,
    string Source);
