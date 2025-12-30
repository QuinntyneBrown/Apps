using SideHustleIncomeTracker.Core;

namespace SideHustleIncomeTracker.Api.Features.TaxEstimates;

public record TaxEstimateDto
{
    public Guid TaxEstimateId { get; init; }
    public Guid BusinessId { get; init; }
    public int TaxYear { get; init; }
    public int Quarter { get; init; }
    public decimal NetProfit { get; init; }
    public decimal SelfEmploymentTax { get; init; }
    public decimal IncomeTax { get; init; }
    public decimal TotalEstimatedTax { get; init; }
    public bool IsPaid { get; init; }
    public DateTime? PaymentDate { get; init; }
}

public static class TaxEstimateExtensions
{
    public static TaxEstimateDto ToDto(this TaxEstimate taxEstimate)
    {
        return new TaxEstimateDto
        {
            TaxEstimateId = taxEstimate.TaxEstimateId,
            BusinessId = taxEstimate.BusinessId,
            TaxYear = taxEstimate.TaxYear,
            Quarter = taxEstimate.Quarter,
            NetProfit = taxEstimate.NetProfit,
            SelfEmploymentTax = taxEstimate.SelfEmploymentTax,
            IncomeTax = taxEstimate.IncomeTax,
            TotalEstimatedTax = taxEstimate.TotalEstimatedTax,
            IsPaid = taxEstimate.IsPaid,
            PaymentDate = taxEstimate.PaymentDate,
        };
    }
}
