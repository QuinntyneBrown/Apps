using CampingTripPlanner.Core;

namespace CampingTripPlanner.Api.Features.Trips;

public record TripDto
{
    public Guid TripId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? CampsiteId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int NumberOfPeople { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class TripExtensions
{
    public static TripDto ToDto(this Trip trip)
    {
        return new TripDto
        {
            TripId = trip.TripId,
            UserId = trip.UserId,
            Name = trip.Name,
            CampsiteId = trip.CampsiteId,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            NumberOfPeople = trip.NumberOfPeople,
            Notes = trip.Notes,
            CreatedAt = trip.CreatedAt,
        };
    }
}
