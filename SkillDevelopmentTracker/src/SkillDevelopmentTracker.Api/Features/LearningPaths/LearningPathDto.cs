using SkillDevelopmentTracker.Core;

namespace SkillDevelopmentTracker.Api.Features.LearningPaths;

public record LearningPathDto
{
    public Guid LearningPathId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public DateTime? CompletionDate { get; init; }
    public List<Guid> CourseIds { get; init; } = new List<Guid>();
    public List<Guid> SkillIds { get; init; } = new List<Guid>();
    public int ProgressPercentage { get; init; }
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class LearningPathExtensions
{
    public static LearningPathDto ToDto(this LearningPath learningPath)
    {
        return new LearningPathDto
        {
            LearningPathId = learningPath.LearningPathId,
            UserId = learningPath.UserId,
            Title = learningPath.Title,
            Description = learningPath.Description,
            StartDate = learningPath.StartDate,
            TargetDate = learningPath.TargetDate,
            CompletionDate = learningPath.CompletionDate,
            CourseIds = learningPath.CourseIds,
            SkillIds = learningPath.SkillIds,
            ProgressPercentage = learningPath.ProgressPercentage,
            IsCompleted = learningPath.IsCompleted,
            Notes = learningPath.Notes,
            CreatedAt = learningPath.CreatedAt,
            UpdatedAt = learningPath.UpdatedAt,
        };
    }
}
