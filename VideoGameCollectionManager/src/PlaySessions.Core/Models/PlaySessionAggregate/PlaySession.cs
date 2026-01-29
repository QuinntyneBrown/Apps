namespace PlaySessions.Core.Models;

public class PlaySession
{
    public Guid SessionId { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
