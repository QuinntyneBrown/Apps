using TravelDestinationWishlist.Core;

namespace TravelDestinationWishlist.Api.Features.Memories;

public record MemoryDto
{
    public Guid MemoryId { get; init; }
    public Guid UserId { get; init; }
    public Guid TripId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime MemoryDate { get; init; }
    public string? PhotoUrl { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class MemoryExtensions
{
    public static MemoryDto ToDto(this Memory memory)
    {
        return new MemoryDto
        {
            MemoryId = memory.MemoryId,
            UserId = memory.UserId,
            TripId = memory.TripId,
            Title = memory.Title,
            Description = memory.Description,
            MemoryDate = memory.MemoryDate,
            PhotoUrl = memory.PhotoUrl,
            CreatedAt = memory.CreatedAt,
        };
    }
}
