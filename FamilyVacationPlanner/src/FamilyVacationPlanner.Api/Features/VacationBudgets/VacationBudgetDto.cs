using FamilyVacationPlanner.Core;

namespace FamilyVacationPlanner.Api.Features.VacationBudgets;

public record VacationBudgetDto
{
    public Guid VacationBudgetId { get; init; }
    public Guid TripId { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal AllocatedAmount { get; init; }
    public decimal? SpentAmount { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class VacationBudgetExtensions
{
    public static VacationBudgetDto ToDto(this VacationBudget budget)
    {
        return new VacationBudgetDto
        {
            VacationBudgetId = budget.VacationBudgetId,
            TripId = budget.TripId,
            Category = budget.Category,
            AllocatedAmount = budget.AllocatedAmount,
            SpentAmount = budget.SpentAmount,
            CreatedAt = budget.CreatedAt,
        };
    }
}
