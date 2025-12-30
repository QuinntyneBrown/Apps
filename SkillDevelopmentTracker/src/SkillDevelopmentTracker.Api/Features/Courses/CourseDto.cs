using SkillDevelopmentTracker.Core;

namespace SkillDevelopmentTracker.Api.Features.Courses;

public record CourseDto
{
    public Guid CourseId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Provider { get; init; } = string.Empty;
    public string? Instructor { get; init; }
    public string? CourseUrl { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? CompletionDate { get; init; }
    public int ProgressPercentage { get; init; }
    public decimal? EstimatedHours { get; init; }
    public decimal ActualHours { get; init; }
    public bool IsCompleted { get; init; }
    public List<Guid> SkillIds { get; init; } = new List<Guid>();
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class CourseExtensions
{
    public static CourseDto ToDto(this Course course)
    {
        return new CourseDto
        {
            CourseId = course.CourseId,
            UserId = course.UserId,
            Title = course.Title,
            Provider = course.Provider,
            Instructor = course.Instructor,
            CourseUrl = course.CourseUrl,
            StartDate = course.StartDate,
            CompletionDate = course.CompletionDate,
            ProgressPercentage = course.ProgressPercentage,
            EstimatedHours = course.EstimatedHours,
            ActualHours = course.ActualHours,
            IsCompleted = course.IsCompleted,
            SkillIds = course.SkillIds,
            Notes = course.Notes,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt,
        };
    }
}
