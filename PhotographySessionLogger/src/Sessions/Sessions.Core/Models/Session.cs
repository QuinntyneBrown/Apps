namespace Sessions.Core.Models;

public class Session
{
    public Guid SessionId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Location { get; private set; } = string.Empty;
    public DateTime SessionDate { get; private set; }
    public string? Description { get; private set; }
    public string SessionType { get; private set; } = string.Empty;
    public string? WeatherConditions { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Session() { }

    public Session(Guid tenantId, Guid userId, string location, DateTime sessionDate, string sessionType, string? description = null, string? weatherConditions = null)
    {
        SessionId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Location = location;
        SessionDate = sessionDate;
        SessionType = sessionType;
        Description = description;
        WeatherConditions = weatherConditions;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string location, DateTime sessionDate, string sessionType, string? description = null, string? weatherConditions = null)
    {
        Location = location;
        SessionDate = sessionDate;
        SessionType = sessionType;
        Description = description;
        WeatherConditions = weatherConditions;
        UpdatedAt = DateTime.UtcNow;
    }
}
