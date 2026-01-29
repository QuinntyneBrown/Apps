namespace Messaging.Contracts.Events;

public sealed record DonationCreatedEvent : IntegrationEvent
{
    public required Guid DonationId { get; init; }
    public required Guid OrganizationId { get; init; }
    public required decimal Amount { get; init; }
}
