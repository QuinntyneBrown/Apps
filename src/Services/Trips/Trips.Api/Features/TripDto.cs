namespace Trips.Api.Features;

public record TripDto
{
    public Guid TripId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Destination { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public DateTime CreatedAt { get; init; }
}
