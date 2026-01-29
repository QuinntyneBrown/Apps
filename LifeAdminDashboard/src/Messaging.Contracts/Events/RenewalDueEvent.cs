namespace Messaging.Contracts.Events;

public sealed record RenewalDueEvent : IntegrationEvent
{
    public required Guid RenewalId { get; init; }
    public required string Name { get; init; }
    public required DateTime RenewalDate { get; init; }
}
