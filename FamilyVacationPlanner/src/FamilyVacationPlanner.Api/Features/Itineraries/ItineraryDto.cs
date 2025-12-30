using FamilyVacationPlanner.Core;

namespace FamilyVacationPlanner.Api.Features.Itineraries;

public record ItineraryDto
{
    public Guid ItineraryId { get; init; }
    public Guid TripId { get; init; }
    public DateTime Date { get; init; }
    public string? Activity { get; init; }
    public string? Location { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ItineraryExtensions
{
    public static ItineraryDto ToDto(this Itinerary itinerary)
    {
        return new ItineraryDto
        {
            ItineraryId = itinerary.ItineraryId,
            TripId = itinerary.TripId,
            Date = itinerary.Date,
            Activity = itinerary.Activity,
            Location = itinerary.Location,
            CreatedAt = itinerary.CreatedAt,
        };
    }
}
