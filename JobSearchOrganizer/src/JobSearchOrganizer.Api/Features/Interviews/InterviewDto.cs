using JobSearchOrganizer.Core;

namespace JobSearchOrganizer.Api.Features.Interviews;

public record InterviewDto
{
    public Guid InterviewId { get; init; }
    public Guid UserId { get; init; }
    public Guid ApplicationId { get; init; }
    public string InterviewType { get; init; } = string.Empty;
    public DateTime ScheduledDateTime { get; init; }
    public int? DurationMinutes { get; init; }
    public List<string> Interviewers { get; init; } = new List<string>();
    public string? Location { get; init; }
    public string? PreparationNotes { get; init; }
    public string? Feedback { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class InterviewExtensions
{
    public static InterviewDto ToDto(this Interview interview)
    {
        return new InterviewDto
        {
            InterviewId = interview.InterviewId,
            UserId = interview.UserId,
            ApplicationId = interview.ApplicationId,
            InterviewType = interview.InterviewType,
            ScheduledDateTime = interview.ScheduledDateTime,
            DurationMinutes = interview.DurationMinutes,
            Interviewers = interview.Interviewers,
            Location = interview.Location,
            PreparationNotes = interview.PreparationNotes,
            Feedback = interview.Feedback,
            IsCompleted = interview.IsCompleted,
            CompletedDate = interview.CompletedDate,
            CreatedAt = interview.CreatedAt,
            UpdatedAt = interview.UpdatedAt,
        };
    }
}
