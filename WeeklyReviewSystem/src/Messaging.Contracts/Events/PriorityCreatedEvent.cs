namespace Messaging.Contracts.Events;
public sealed record PriorityCreatedEvent : IntegrationEvent { public required Guid PriorityId { get; init; } public required string Title { get; init; } }
