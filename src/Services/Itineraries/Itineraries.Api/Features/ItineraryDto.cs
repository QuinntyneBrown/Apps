namespace Itineraries.Api.Features;

public record ItineraryDto
{
    public Guid ItineraryId { get; init; }
    public Guid TripId { get; init; }
    public DateTime Date { get; init; }
    public string? Activity { get; init; }
    public string? Location { get; init; }
    public DateTime CreatedAt { get; init; }
}
