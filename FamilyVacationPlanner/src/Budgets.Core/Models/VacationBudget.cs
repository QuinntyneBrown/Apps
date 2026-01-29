namespace Budgets.Core.Models;

public class VacationBudget
{
    public Guid VacationBudgetId { get; set; }
    public Guid TenantId { get; set; }
    public Guid TripId { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal AllocatedAmount { get; set; }
    public decimal? SpentAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
