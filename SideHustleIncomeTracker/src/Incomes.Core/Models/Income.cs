namespace Incomes.Core.Models;

public class Income
{
    public Guid IncomeId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BusinessId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string? Source { get; private set; }
    public DateTime IncomeDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Income() { }

    public Income(Guid tenantId, Guid userId, Guid businessId, string description, decimal amount, DateTime incomeDate, string? source = null)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));

        IncomeId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BusinessId = businessId;
        Description = description;
        Amount = amount;
        IncomeDate = incomeDate;
        Source = source;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? description = null, decimal? amount = null, DateTime? incomeDate = null, string? source = null)
    {
        if (description != null) Description = description;
        if (amount.HasValue && amount.Value > 0) Amount = amount.Value;
        if (incomeDate.HasValue) IncomeDate = incomeDate.Value;
        if (source != null) Source = source;
    }
}
