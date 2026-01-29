namespace Messaging.Contracts.Events;
public sealed record AccomplishmentCreatedEvent : IntegrationEvent { public required Guid AccomplishmentId { get; init; } public required string Description { get; init; } }
