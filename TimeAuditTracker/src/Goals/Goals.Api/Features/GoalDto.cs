using Goals.Core.Models;

namespace Goals.Api.Features;

public record GoalDto
{
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Category { get; init; } = string.Empty;
    public int TargetMinutesPerDay { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class GoalDtoExtensions
{
    public static GoalDto ToDto(this Goal goal) => new()
    {
        GoalId = goal.GoalId,
        UserId = goal.UserId,
        TenantId = goal.TenantId,
        Category = goal.Category,
        TargetMinutesPerDay = goal.TargetMinutesPerDay,
        IsActive = goal.IsActive,
        Notes = goal.Notes,
        CreatedAt = goal.CreatedAt
    };
}
