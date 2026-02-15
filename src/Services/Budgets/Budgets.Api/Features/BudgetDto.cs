namespace Budgets.Api.Features;

public record BudgetDto
{
    public Guid VacationBudgetId { get; init; }
    public Guid TripId { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal AllocatedAmount { get; init; }
    public decimal? SpentAmount { get; init; }
    public DateTime CreatedAt { get; init; }
}
