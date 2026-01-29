namespace Activities.Core.Models;

public class Activity
{
    public Guid ActivityId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ActivityType Type { get; set; } = ActivityType.Sports;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? CoachName { get; set; }
    public string? ContactInfo { get; set; }
    public decimal? Cost { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum ActivityType
{
    Sports,
    Music,
    Art,
    Dance,
    Swimming,
    MartialArts,
    Tutoring,
    Other
}
