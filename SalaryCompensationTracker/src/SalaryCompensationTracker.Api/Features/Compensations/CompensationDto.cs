using SalaryCompensationTracker.Core;

namespace SalaryCompensationTracker.Api.Features.Compensations;

public record CompensationDto
{
    public Guid CompensationId { get; init; }
    public Guid UserId { get; init; }
    public CompensationType CompensationType { get; init; }
    public string Employer { get; init; } = string.Empty;
    public string JobTitle { get; init; } = string.Empty;
    public decimal BaseSalary { get; init; }
    public string Currency { get; init; } = "USD";
    public decimal? Bonus { get; init; }
    public decimal? StockValue { get; init; }
    public decimal? OtherCompensation { get; init; }
    public decimal TotalCompensation { get; init; }
    public DateTime EffectiveDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class CompensationExtensions
{
    public static CompensationDto ToDto(this Compensation compensation)
    {
        return new CompensationDto
        {
            CompensationId = compensation.CompensationId,
            UserId = compensation.UserId,
            CompensationType = compensation.CompensationType,
            Employer = compensation.Employer,
            JobTitle = compensation.JobTitle,
            BaseSalary = compensation.BaseSalary,
            Currency = compensation.Currency,
            Bonus = compensation.Bonus,
            StockValue = compensation.StockValue,
            OtherCompensation = compensation.OtherCompensation,
            TotalCompensation = compensation.TotalCompensation,
            EffectiveDate = compensation.EffectiveDate,
            EndDate = compensation.EndDate,
            Notes = compensation.Notes,
            CreatedAt = compensation.CreatedAt,
            UpdatedAt = compensation.UpdatedAt,
        };
    }
}
