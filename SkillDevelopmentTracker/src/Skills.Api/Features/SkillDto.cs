namespace Skills.Api.Features;

public record SkillDto(Guid SkillId, Guid TenantId, Guid UserId, string Name, string? Description, int ProficiencyLevel, DateTime CreatedAt, DateTime? UpdatedAt);
