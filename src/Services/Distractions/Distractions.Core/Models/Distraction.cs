namespace Distractions.Core.Models;

public class Distraction
{
    public Guid DistractionId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid SessionId { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public int DurationSeconds { get; private set; }

    private Distraction() { }

    public Distraction(Guid tenantId, Guid sessionId, string type, string? description, int durationSeconds)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Type cannot be empty.", nameof(type));

        DistractionId = Guid.NewGuid();
        TenantId = tenantId;
        SessionId = sessionId;
        Type = type;
        Description = description;
        OccurredAt = DateTime.UtcNow;
        DurationSeconds = durationSeconds;
    }

    public void Update(string? type = null, string? description = null, int? durationSeconds = null)
    {
        if (type != null)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type cannot be empty.", nameof(type));
            Type = type;
        }

        if (description != null)
            Description = description;

        if (durationSeconds.HasValue)
            DurationSeconds = durationSeconds.Value;
    }
}
