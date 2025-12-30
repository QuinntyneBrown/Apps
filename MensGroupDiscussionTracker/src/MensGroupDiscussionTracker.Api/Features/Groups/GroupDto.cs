using MensGroupDiscussionTracker.Core;

namespace MensGroupDiscussionTracker.Api.Features.Groups;

public record GroupDto
{
    public Guid GroupId { get; init; }
    public Guid CreatedByUserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? MeetingSchedule { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class GroupExtensions
{
    public static GroupDto ToDto(this Group group)
    {
        return new GroupDto
        {
            GroupId = group.GroupId,
            CreatedByUserId = group.CreatedByUserId,
            Name = group.Name,
            Description = group.Description,
            MeetingSchedule = group.MeetingSchedule,
            IsActive = group.IsActive,
            CreatedAt = group.CreatedAt,
            UpdatedAt = group.UpdatedAt,
        };
    }
}
