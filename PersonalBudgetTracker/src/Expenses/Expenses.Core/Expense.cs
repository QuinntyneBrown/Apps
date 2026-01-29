namespace Expenses.Core;

public class Expense
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? BudgetId { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
