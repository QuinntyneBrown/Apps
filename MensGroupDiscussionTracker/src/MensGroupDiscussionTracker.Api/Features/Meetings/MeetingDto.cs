using MensGroupDiscussionTracker.Core;

namespace MensGroupDiscussionTracker.Api.Features.Meetings;

public record MeetingDto
{
    public Guid MeetingId { get; init; }
    public Guid GroupId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime MeetingDateTime { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public int AttendeeCount { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class MeetingExtensions
{
    public static MeetingDto ToDto(this Meeting meeting)
    {
        return new MeetingDto
        {
            MeetingId = meeting.MeetingId,
            GroupId = meeting.GroupId,
            Title = meeting.Title,
            MeetingDateTime = meeting.MeetingDateTime,
            Location = meeting.Location,
            Notes = meeting.Notes,
            AttendeeCount = meeting.AttendeeCount,
            CreatedAt = meeting.CreatedAt,
            UpdatedAt = meeting.UpdatedAt,
        };
    }
}
