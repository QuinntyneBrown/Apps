namespace Messaging.Contracts.Events;

public sealed record BeneficiaryCreatedEvent : IntegrationEvent
{
    public required Guid BeneficiaryId { get; init; }
    public required string Name { get; init; }
}
