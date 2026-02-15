namespace Achievements.Core.Models;

public class Achievement
{
    public Guid AchievementId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AchievementType AchievementType { get; set; }
    public DateTime AchievedDate { get; set; }
    public string? Organization { get; set; }
    public string? Impact { get; set; }
    public bool IsFeatured { get; set; }
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
        {
            Tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

public enum AchievementType
{
    Award = 0,
    Certification = 1,
    Publication = 2,
    Presentation = 3,
    ProjectMilestone = 4,
    Promotion = 5,
    FinancialImpact = 6,
    Leadership = 7,
    Innovation = 8,
    Other = 9,
}
