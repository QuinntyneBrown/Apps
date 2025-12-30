using TravelDestinationWishlist.Core;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record DestinationDto
{
    public Guid DestinationId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public DestinationType DestinationType { get; init; }
    public string? Description { get; init; }
    public int Priority { get; init; }
    public bool IsVisited { get; init; }
    public DateTime? VisitedDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class DestinationExtensions
{
    public static DestinationDto ToDto(this Destination destination)
    {
        return new DestinationDto
        {
            DestinationId = destination.DestinationId,
            UserId = destination.UserId,
            Name = destination.Name,
            Country = destination.Country,
            DestinationType = destination.DestinationType,
            Description = destination.Description,
            Priority = destination.Priority,
            IsVisited = destination.IsVisited,
            VisitedDate = destination.VisitedDate,
            Notes = destination.Notes,
            CreatedAt = destination.CreatedAt,
        };
    }
}
