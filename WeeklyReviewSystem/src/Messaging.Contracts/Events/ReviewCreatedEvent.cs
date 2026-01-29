namespace Messaging.Contracts.Events;
public sealed record ReviewCreatedEvent : IntegrationEvent { public required Guid ReviewId { get; init; } public required DateTime WeekStartDate { get; init; } }
