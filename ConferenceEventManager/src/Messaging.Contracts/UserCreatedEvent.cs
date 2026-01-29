namespace Messaging.Contracts;

public record UserCreatedEvent : IntegrationEvent
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}
