using StressMoodTracker.Core;

namespace StressMoodTracker.Api.Features.Triggers;

public record TriggerDto
{
    public Guid TriggerId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string TriggerType { get; init; } = string.Empty;
    public int ImpactLevel { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class TriggerExtensions
{
    public static TriggerDto ToDto(this Trigger trigger)
    {
        return new TriggerDto
        {
            TriggerId = trigger.TriggerId,
            UserId = trigger.UserId,
            Name = trigger.Name,
            Description = trigger.Description,
            TriggerType = trigger.TriggerType,
            ImpactLevel = trigger.ImpactLevel,
            CreatedAt = trigger.CreatedAt,
        };
    }
}
