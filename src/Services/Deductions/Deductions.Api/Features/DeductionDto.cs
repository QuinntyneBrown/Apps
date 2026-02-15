using Deductions.Core.Models;

namespace Deductions.Api.Features;

public record DeductionDto
{
    public Guid DeductionId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public Guid TaxYearId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public DeductionCategory Category { get; init; }
    public string? Notes { get; init; }
    public bool HasReceipt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class DeductionDtoExtensions
{
    public static DeductionDto ToDto(this Deduction deduction) => new()
    {
        DeductionId = deduction.DeductionId,
        TenantId = deduction.TenantId,
        UserId = deduction.UserId,
        TaxYearId = deduction.TaxYearId,
        Description = deduction.Description,
        Amount = deduction.Amount,
        Date = deduction.Date,
        Category = deduction.Category,
        Notes = deduction.Notes,
        HasReceipt = deduction.HasReceipt,
        CreatedAt = deduction.CreatedAt
    };
}
