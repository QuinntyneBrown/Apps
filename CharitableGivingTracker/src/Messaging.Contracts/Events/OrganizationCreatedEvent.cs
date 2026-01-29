namespace Messaging.Contracts.Events;

public sealed record OrganizationCreatedEvent : IntegrationEvent
{
    public required Guid OrganizationId { get; init; }
    public required string Name { get; init; }
}
