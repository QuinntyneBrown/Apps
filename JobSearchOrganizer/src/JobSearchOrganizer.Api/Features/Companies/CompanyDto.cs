using JobSearchOrganizer.Core;

namespace JobSearchOrganizer.Api.Features.Companies;

public record CompanyDto
{
    public Guid CompanyId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Industry { get; init; }
    public string? Website { get; init; }
    public string? Location { get; init; }
    public string? CompanySize { get; init; }
    public string? CultureNotes { get; init; }
    public string? ResearchNotes { get; init; }
    public bool IsTargetCompany { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class CompanyExtensions
{
    public static CompanyDto ToDto(this Company company)
    {
        return new CompanyDto
        {
            CompanyId = company.CompanyId,
            UserId = company.UserId,
            Name = company.Name,
            Industry = company.Industry,
            Website = company.Website,
            Location = company.Location,
            CompanySize = company.CompanySize,
            CultureNotes = company.CultureNotes,
            ResearchNotes = company.ResearchNotes,
            IsTargetCompany = company.IsTargetCompany,
            CreatedAt = company.CreatedAt,
            UpdatedAt = company.UpdatedAt,
        };
    }
}
