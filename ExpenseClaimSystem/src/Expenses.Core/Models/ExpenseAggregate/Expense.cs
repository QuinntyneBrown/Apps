namespace Expenses.Core.Models;

public class Expense
{
    public Guid ExpenseId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public DateTime ExpenseDate { get; private set; }
    public string? ReceiptUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Expense() { }

    public Expense(Guid tenantId, Guid userId, string description, decimal amount, string category, DateTime expenseDate, string? receiptUrl = null)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be empty.", nameof(category));

        ExpenseId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Description = description;
        Amount = amount;
        Category = category;
        ExpenseDate = expenseDate;
        ReceiptUrl = receiptUrl;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? description = null, decimal? amount = null, string? category = null, DateTime? expenseDate = null, string? receiptUrl = null)
    {
        if (description != null)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.", nameof(description));
            Description = description;
        }

        if (amount.HasValue)
        {
            if (amount.Value <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            Amount = amount.Value;
        }

        if (category != null)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be empty.", nameof(category));
            Category = category;
        }

        if (expenseDate.HasValue)
            ExpenseDate = expenseDate.Value;

        if (receiptUrl != null)
            ReceiptUrl = receiptUrl;
    }
}
