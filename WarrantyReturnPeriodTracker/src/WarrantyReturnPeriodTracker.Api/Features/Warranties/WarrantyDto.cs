using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Warranties;

public class WarrantyDto
{
    public Guid WarrantyId { get; set; }
    public Guid PurchaseId { get; set; }
    public WarrantyType WarrantyType { get; set; }
    public string Provider { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationMonths { get; set; }
    public WarrantyStatus Status { get; set; }
    public string? CoverageDetails { get; set; }
    public string? Terms { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Notes { get; set; }
    public DateTime? ClaimFiledDate { get; set; }
}

public static class WarrantyDtoExtensions
{
    public static WarrantyDto ToDto(this Warranty warranty)
    {
        return new WarrantyDto
        {
            WarrantyId = warranty.WarrantyId,
            PurchaseId = warranty.PurchaseId,
            WarrantyType = warranty.WarrantyType,
            Provider = warranty.Provider,
            StartDate = warranty.StartDate,
            EndDate = warranty.EndDate,
            DurationMonths = warranty.DurationMonths,
            Status = warranty.Status,
            CoverageDetails = warranty.CoverageDetails,
            Terms = warranty.Terms,
            RegistrationNumber = warranty.RegistrationNumber,
            Notes = warranty.Notes,
            ClaimFiledDate = warranty.ClaimFiledDate
        };
    }
}
