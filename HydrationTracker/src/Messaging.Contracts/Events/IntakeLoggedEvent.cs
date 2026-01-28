namespace Messaging.Contracts.Events;

public sealed record IntakeLoggedEvent : IntegrationEvent
{
    public required Guid IntakeId { get; init; }
    public required int AmountMl { get; init; }
    public required string BeverageType { get; init; }
    public required DateTime LoggedAt { get; init; }
}
