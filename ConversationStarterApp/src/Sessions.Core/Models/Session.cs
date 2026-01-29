namespace Sessions.Core.Models;

public class Session
{
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAt { get; set; }
    public int PromptsUsed { get; set; }
    public string? Notes { get; set; }
}
