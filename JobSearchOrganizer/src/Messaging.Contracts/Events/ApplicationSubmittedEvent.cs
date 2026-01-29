namespace Messaging.Contracts.Events;

public sealed record ApplicationSubmittedEvent : IntegrationEvent
{
    public required Guid ApplicationId { get; init; }
    public required Guid CompanyId { get; init; }
    public required string JobTitle { get; init; }
}
