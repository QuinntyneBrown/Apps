using SleepQualityTracker.Core;

namespace SleepQualityTracker.Api.Features.Patterns;

public record PatternDto
{
    public Guid PatternId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string PatternType { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int ConfidenceLevel { get; init; }
    public string? Insights { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsHighConfidence { get; init; }
    public int DurationDays { get; init; }
}

public static class PatternExtensions
{
    public static PatternDto ToDto(this Pattern pattern)
    {
        return new PatternDto
        {
            PatternId = pattern.PatternId,
            UserId = pattern.UserId,
            Name = pattern.Name,
            Description = pattern.Description,
            PatternType = pattern.PatternType,
            StartDate = pattern.StartDate,
            EndDate = pattern.EndDate,
            ConfidenceLevel = pattern.ConfidenceLevel,
            Insights = pattern.Insights,
            CreatedAt = pattern.CreatedAt,
            IsHighConfidence = pattern.IsHighConfidence(),
            DurationDays = pattern.GetDuration(),
        };
    }
}
