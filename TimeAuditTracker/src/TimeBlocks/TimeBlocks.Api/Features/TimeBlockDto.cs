using TimeBlocks.Core.Models;

namespace TimeBlocks.Api.Features;

public record TimeBlockDto
{
    public Guid TimeBlockId { get; init; }
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public ActivityCategory Category { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
    public string? Tags { get; init; }
    public bool IsProductive { get; init; }
    public double? DurationMinutes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class TimeBlockDtoExtensions
{
    public static TimeBlockDto ToDto(this TimeBlock timeBlock) => new()
    {
        TimeBlockId = timeBlock.TimeBlockId,
        UserId = timeBlock.UserId,
        TenantId = timeBlock.TenantId,
        Category = timeBlock.Category,
        Description = timeBlock.Description,
        StartTime = timeBlock.StartTime,
        EndTime = timeBlock.EndTime,
        Notes = timeBlock.Notes,
        Tags = timeBlock.Tags,
        IsProductive = timeBlock.IsProductive,
        DurationMinutes = timeBlock.GetDurationInMinutes(),
        CreatedAt = timeBlock.CreatedAt
    };
}
