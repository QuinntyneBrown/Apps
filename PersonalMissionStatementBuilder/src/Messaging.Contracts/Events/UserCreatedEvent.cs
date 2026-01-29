namespace Messaging.Contracts.Events;

public record UserCreatedEvent : IntegrationEvent
{
    public required string Email { get; init; }
    public required string Username { get; init; }
}
