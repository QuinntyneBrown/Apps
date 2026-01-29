namespace Messaging.Contracts.Events;

public sealed record AssignmentCreatedEvent : IntegrationEvent
{
    public required Guid AssignmentId { get; init; }
    public required Guid ChoreId { get; init; }
    public required Guid FamilyMemberId { get; init; }
}
