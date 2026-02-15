using Interviews.Core.Models;

namespace Interviews.Api.Features;

public record InterviewDto(
    Guid InterviewId,
    Guid UserId,
    Guid ApplicationId,
    DateTime ScheduledDate,
    InterviewType Type,
    string? InterviewerName,
    string? Location,
    string? MeetingLink,
    string? Notes,
    InterviewStatus Status,
    string? Feedback,
    DateTime CreatedAt);

public static class InterviewExtensions
{
    public static InterviewDto ToDto(this Interview interview)
    {
        return new InterviewDto(
            interview.InterviewId,
            interview.UserId,
            interview.ApplicationId,
            interview.ScheduledDate,
            interview.Type,
            interview.InterviewerName,
            interview.Location,
            interview.MeetingLink,
            interview.Notes,
            interview.Status,
            interview.Feedback,
            interview.CreatedAt);
    }
}
