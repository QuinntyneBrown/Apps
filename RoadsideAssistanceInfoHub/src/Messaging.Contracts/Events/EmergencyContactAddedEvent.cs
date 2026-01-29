namespace Messaging.Contracts.Events;

public sealed record EmergencyContactAddedEvent : IntegrationEvent
{
    public required Guid EmergencyContactId { get; init; }
    public required string Name { get; init; }
    public required string PhoneNumber { get; init; }
    public required bool IsPrimaryContact { get; init; }
}
