using BucketListManager.Core;

namespace BucketListManager.Api.Features.BucketListItems;

public record BucketListItemDto
{
    public Guid BucketListItemId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Category Category { get; init; }
    public Priority Priority { get; init; }
    public ItemStatus Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class BucketListItemExtensions
{
    public static BucketListItemDto ToDto(this BucketListItem item)
    {
        return new BucketListItemDto
        {
            BucketListItemId = item.BucketListItemId,
            UserId = item.UserId,
            Title = item.Title,
            Description = item.Description,
            Category = item.Category,
            Priority = item.Priority,
            Status = item.Status,
            TargetDate = item.TargetDate,
            CompletedDate = item.CompletedDate,
            Notes = item.Notes,
            CreatedAt = item.CreatedAt,
        };
    }
}
