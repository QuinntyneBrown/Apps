namespace Messaging.Contracts.Events;

public sealed record UserCreatedEvent : IntegrationEvent
{
    public required string Username { get; init; }
    public required string Email { get; init; }
}
