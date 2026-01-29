using TaxYears.Core.Models;

namespace TaxYears.Api.Features;

public record TaxYearDto
{
    public Guid TaxYearId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public int Year { get; init; }
    public bool IsClosed { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class TaxYearDtoExtensions
{
    public static TaxYearDto ToDto(this TaxYear taxYear) => new()
    {
        TaxYearId = taxYear.TaxYearId,
        TenantId = taxYear.TenantId,
        UserId = taxYear.UserId,
        Year = taxYear.Year,
        IsClosed = taxYear.IsClosed,
        Notes = taxYear.Notes,
        CreatedAt = taxYear.CreatedAt
    };
}
