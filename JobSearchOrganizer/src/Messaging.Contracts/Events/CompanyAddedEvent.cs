namespace Messaging.Contracts.Events;

public sealed record CompanyAddedEvent : IntegrationEvent
{
    public required Guid CompanyId { get; init; }
    public required string Name { get; init; }
}
