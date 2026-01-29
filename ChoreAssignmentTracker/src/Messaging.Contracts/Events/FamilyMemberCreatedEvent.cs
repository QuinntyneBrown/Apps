namespace Messaging.Contracts.Events;

public sealed record FamilyMemberCreatedEvent : IntegrationEvent
{
    public required Guid FamilyMemberId { get; init; }
    public required string Name { get; init; }
}
