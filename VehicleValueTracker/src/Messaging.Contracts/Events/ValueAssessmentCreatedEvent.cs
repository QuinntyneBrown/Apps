namespace Messaging.Contracts.Events;

public sealed record ValueAssessmentCreatedEvent : IntegrationEvent
{
    public required Guid AssessmentId { get; init; }
    public required Guid VehicleId { get; init; }
    public required decimal EstimatedValue { get; init; }
}
