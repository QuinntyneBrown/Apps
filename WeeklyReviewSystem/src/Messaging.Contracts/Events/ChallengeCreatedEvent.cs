namespace Messaging.Contracts.Events;
public sealed record ChallengeCreatedEvent : IntegrationEvent { public required Guid ChallengeId { get; init; } public required string Description { get; init; } }
