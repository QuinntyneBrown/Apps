using SalaryCompensationTracker.Core;

namespace SalaryCompensationTracker.Api.Features.MarketComparisons;

public record MarketComparisonDto
{
    public Guid MarketComparisonId { get; init; }
    public Guid UserId { get; init; }
    public string JobTitle { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string? ExperienceLevel { get; init; }
    public decimal? MinSalary { get; init; }
    public decimal? MaxSalary { get; init; }
    public decimal? MedianSalary { get; init; }
    public string? DataSource { get; init; }
    public DateTime ComparisonDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class MarketComparisonExtensions
{
    public static MarketComparisonDto ToDto(this MarketComparison marketComparison)
    {
        return new MarketComparisonDto
        {
            MarketComparisonId = marketComparison.MarketComparisonId,
            UserId = marketComparison.UserId,
            JobTitle = marketComparison.JobTitle,
            Location = marketComparison.Location,
            ExperienceLevel = marketComparison.ExperienceLevel,
            MinSalary = marketComparison.MinSalary,
            MaxSalary = marketComparison.MaxSalary,
            MedianSalary = marketComparison.MedianSalary,
            DataSource = marketComparison.DataSource,
            ComparisonDate = marketComparison.ComparisonDate,
            Notes = marketComparison.Notes,
            CreatedAt = marketComparison.CreatedAt,
            UpdatedAt = marketComparison.UpdatedAt,
        };
    }
}
