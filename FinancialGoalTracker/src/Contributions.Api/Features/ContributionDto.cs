namespace Contributions.Api.Features;

public record ContributionDto
{
    public Guid ContributionId { get; init; }
    public Guid GoalId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}
