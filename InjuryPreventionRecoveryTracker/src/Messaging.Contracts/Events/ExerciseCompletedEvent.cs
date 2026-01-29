namespace Messaging.Contracts.Events;

public sealed record ExerciseCompletedEvent : IntegrationEvent
{
    public required Guid ExerciseId { get; init; }
    public required Guid InjuryId { get; init; }
    public required string ExerciseName { get; init; }
}
