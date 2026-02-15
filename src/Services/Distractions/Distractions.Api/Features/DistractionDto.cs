namespace Distractions.Api.Features;

public record DistractionDto
{
    public Guid DistractionId { get; init; }
    public Guid SessionId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime OccurredAt { get; init; }
    public int DurationSeconds { get; init; }
}
