namespace Skills.Core.Models;

public class Skill
{
    public Guid SkillId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int ProficiencyLevel { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Skill() { }

    public Skill(Guid tenantId, Guid userId, string name, string? description = null, int proficiencyLevel = 1)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Skill name cannot be empty.", nameof(name));

        SkillId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        Description = description;
        ProficiencyLevel = Math.Clamp(proficiencyLevel, 1, 10);
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, string? description = null, int? proficiencyLevel = null)
    {
        if (name != null) Name = name;
        if (description != null) Description = description;
        if (proficiencyLevel.HasValue) ProficiencyLevel = Math.Clamp(proficiencyLevel.Value, 1, 10);
        UpdatedAt = DateTime.UtcNow;
    }
}
