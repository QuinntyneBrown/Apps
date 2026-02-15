using Companies.Core.Models;

namespace Companies.Api.Features;

public record CompanyDto(
    Guid CompanyId,
    Guid UserId,
    string Name,
    string? Website,
    string? Industry,
    string? Location,
    string? Size,
    string? Description,
    string? Notes,
    int? Rating,
    DateTime CreatedAt);

public static class CompanyExtensions
{
    public static CompanyDto ToDto(this Company company)
    {
        return new CompanyDto(
            company.CompanyId,
            company.UserId,
            company.Name,
            company.Website,
            company.Industry,
            company.Location,
            company.Size,
            company.Description,
            company.Notes,
            company.Rating,
            company.CreatedAt);
    }
}
