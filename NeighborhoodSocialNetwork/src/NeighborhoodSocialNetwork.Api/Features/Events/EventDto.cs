using NeighborhoodSocialNetwork.Core;

namespace NeighborhoodSocialNetwork.Api.Features.Events;

public record EventDto
{
    public Guid EventId { get; init; }
    public Guid CreatedByNeighborId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime EventDateTime { get; init; }
    public string? Location { get; init; }
    public bool IsPublic { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class EventExtensions
{
    public static EventDto ToDto(this Event @event)
    {
        return new EventDto
        {
            EventId = @event.EventId,
            CreatedByNeighborId = @event.CreatedByNeighborId,
            Title = @event.Title,
            Description = @event.Description,
            EventDateTime = @event.EventDateTime,
            Location = @event.Location,
            IsPublic = @event.IsPublic,
            CreatedAt = @event.CreatedAt,
            UpdatedAt = @event.UpdatedAt,
        };
    }
}
