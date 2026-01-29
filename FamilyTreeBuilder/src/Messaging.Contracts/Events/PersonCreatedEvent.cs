namespace Messaging.Contracts.Events;

public sealed record PersonCreatedEvent : IntegrationEvent
{
    public required Guid PersonId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}
