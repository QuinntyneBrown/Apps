using SalaryCompensationTracker.Core;

namespace SalaryCompensationTracker.Api.Features.Benefits;

public record BenefitDto
{
    public Guid BenefitId { get; init; }
    public Guid UserId { get; init; }
    public Guid? CompensationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? EstimatedValue { get; init; }
    public decimal? EmployerContribution { get; init; }
    public decimal? EmployeeContribution { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class BenefitExtensions
{
    public static BenefitDto ToDto(this Benefit benefit)
    {
        return new BenefitDto
        {
            BenefitId = benefit.BenefitId,
            UserId = benefit.UserId,
            CompensationId = benefit.CompensationId,
            Name = benefit.Name,
            Category = benefit.Category,
            Description = benefit.Description,
            EstimatedValue = benefit.EstimatedValue,
            EmployerContribution = benefit.EmployerContribution,
            EmployeeContribution = benefit.EmployeeContribution,
            CreatedAt = benefit.CreatedAt,
            UpdatedAt = benefit.UpdatedAt,
        };
    }
}
