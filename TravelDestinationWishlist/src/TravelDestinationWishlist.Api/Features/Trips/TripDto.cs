using TravelDestinationWishlist.Core;

namespace TravelDestinationWishlist.Api.Features.Trips;

public record TripDto
{
    public Guid TripId { get; init; }
    public Guid UserId { get; init; }
    public Guid DestinationId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal? TotalCost { get; init; }
    public string? Accommodation { get; init; }
    public string? Transportation { get; init; }
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
            DestinationId = trip.DestinationId,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            TotalCost = trip.TotalCost,
            Accommodation = trip.Accommodation,
            Transportation = trip.Transportation,
            Notes = trip.Notes,
            CreatedAt = trip.CreatedAt,
        };
    }
}
