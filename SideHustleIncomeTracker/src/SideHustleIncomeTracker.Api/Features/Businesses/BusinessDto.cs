using SideHustleIncomeTracker.Core;

namespace SideHustleIncomeTracker.Api.Features.Businesses;

public record BusinessDto
{
    public Guid BusinessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public bool IsActive { get; init; }
    public string? TaxId { get; init; }
    public string? Notes { get; init; }
    public decimal TotalIncome { get; init; }
    public decimal TotalExpenses { get; init; }
    public decimal NetProfit { get; init; }
}

public static class BusinessExtensions
{
    public static BusinessDto ToDto(this Business business)
    {
        return new BusinessDto
        {
            BusinessId = business.BusinessId,
            Name = business.Name,
            Description = business.Description,
            StartDate = business.StartDate,
            IsActive = business.IsActive,
            TaxId = business.TaxId,
            Notes = business.Notes,
            TotalIncome = business.TotalIncome,
            TotalExpenses = business.TotalExpenses,
            NetProfit = business.CalculateNetProfit(),
        };
    }
}
