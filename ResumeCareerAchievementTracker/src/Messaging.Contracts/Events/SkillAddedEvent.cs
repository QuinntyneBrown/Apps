namespace Messaging.Contracts.Events;

public sealed record SkillAddedEvent : IntegrationEvent
{
    public required Guid SkillId { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }
    public required string ProficiencyLevel { get; init; }
}
