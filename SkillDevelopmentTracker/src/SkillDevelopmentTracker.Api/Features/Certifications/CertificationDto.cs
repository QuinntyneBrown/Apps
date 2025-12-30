using SkillDevelopmentTracker.Core;

namespace SkillDevelopmentTracker.Api.Features.Certifications;

public record CertificationDto
{
    public Guid CertificationId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string IssuingOrganization { get; init; } = string.Empty;
    public DateTime IssueDate { get; init; }
    public DateTime? ExpirationDate { get; init; }
    public string? CredentialId { get; init; }
    public string? CredentialUrl { get; init; }
    public bool IsActive { get; init; }
    public List<Guid> SkillIds { get; init; } = new List<Guid>();
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class CertificationExtensions
{
    public static CertificationDto ToDto(this Certification certification)
    {
        return new CertificationDto
        {
            CertificationId = certification.CertificationId,
            UserId = certification.UserId,
            Name = certification.Name,
            IssuingOrganization = certification.IssuingOrganization,
            IssueDate = certification.IssueDate,
            ExpirationDate = certification.ExpirationDate,
            CredentialId = certification.CredentialId,
            CredentialUrl = certification.CredentialUrl,
            IsActive = certification.IsActive,
            SkillIds = certification.SkillIds,
            Notes = certification.Notes,
            CreatedAt = certification.CreatedAt,
            UpdatedAt = certification.UpdatedAt,
        };
    }
}
