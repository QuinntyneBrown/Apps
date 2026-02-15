namespace Expenses.Core.Models;

public class Expense
{
    public Guid ExpenseId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BusinessId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string? Category { get; private set; }
    public DateTime ExpenseDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Expense() { }

    public Expense(Guid tenantId, Guid userId, Guid businessId, string description, decimal amount, DateTime expenseDate, string? category = null)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));

        ExpenseId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BusinessId = businessId;
        Description = description;
        Amount = amount;
        ExpenseDate = expenseDate;
        Category = category;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? description = null, decimal? amount = null, DateTime? expenseDate = null, string? category = null)
    {
        if (description != null) Description = description;
        if (amount.HasValue && amount.Value > 0) Amount = amount.Value;
        if (expenseDate.HasValue) ExpenseDate = expenseDate.Value;
        if (category != null) Category = category;
    }
}
