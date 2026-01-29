using Warranties.Core.Models;

namespace Warranties.Api.Features;

public record WarrantyDto
{
    public Guid WarrantyId { get; init; }
    public Guid ApplianceId { get; init; }
    public string? Provider { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? CoverageDetails { get; init; }
    public string? DocumentUrl { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class WarrantyExtensions
{
    public static WarrantyDto ToDto(this Warranty warranty) => new()
    {
        WarrantyId = warranty.WarrantyId,
        ApplianceId = warranty.ApplianceId,
        Provider = warranty.Provider,
        StartDate = warranty.StartDate,
        EndDate = warranty.EndDate,
        CoverageDetails = warranty.CoverageDetails,
        DocumentUrl = warranty.DocumentUrl,
        CreatedAt = warranty.CreatedAt
    };
}
