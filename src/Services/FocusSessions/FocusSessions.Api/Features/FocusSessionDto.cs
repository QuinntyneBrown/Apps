namespace FocusSessions.Api.Features;

public record FocusSessionDto
{
    public Guid SessionId { get; init; }
    public Guid UserId { get; init; }
    public string? TaskDescription { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public int PlannedDurationMinutes { get; init; }
    public int ActualDurationMinutes { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
