using FamilyVacationPlanner.Core;

namespace FamilyVacationPlanner.Api.Features.Trips;

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

public static class TripExtensions
{
    public static TripDto ToDto(this Trip trip)
    {
        return new TripDto
        {
            TripId = trip.TripId,
            UserId = trip.UserId,
            Name = trip.Name,
            Destination = trip.Destination,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            CreatedAt = trip.CreatedAt,
        };
    }
}
