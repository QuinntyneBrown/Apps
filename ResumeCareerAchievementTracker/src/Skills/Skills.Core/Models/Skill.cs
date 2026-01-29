namespace Skills.Core.Models;

public class Skill
{
    public Guid SkillId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string ProficiencyLevel { get; set; } = string.Empty;
    public decimal? YearsOfExperience { get; set; }
    public DateTime? LastUsedDate { get; set; }
    public string? Notes { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void UpdateProficiency(string level)
    {
        ProficiencyLevel = level;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
        UpdatedAt = DateTime.UtcNow;
    }
}
