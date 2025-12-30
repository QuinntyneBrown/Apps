using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Features.Progresses;

public record ProgressDto
{
    public Guid ProgressId { get; init; }
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public DateTime ProgressDate { get; init; }
    public string Notes { get; init; } = string.Empty;
    public double CompletionPercentage { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ProgressExtensions
{
    public static ProgressDto ToDto(this Progress progress)
    {
        return new ProgressDto
        {
            ProgressId = progress.ProgressId,
            GoalId = progress.GoalId,
            UserId = progress.UserId,
            ProgressDate = progress.ProgressDate,
            Notes = progress.Notes,
            CompletionPercentage = progress.CompletionPercentage,
            CreatedAt = progress.CreatedAt,
            UpdatedAt = progress.UpdatedAt,
        };
    }
}
