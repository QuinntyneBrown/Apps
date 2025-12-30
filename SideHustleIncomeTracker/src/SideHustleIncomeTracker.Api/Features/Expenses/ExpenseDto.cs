using SideHustleIncomeTracker.Core;

namespace SideHustleIncomeTracker.Api.Features.Expenses;

public record ExpenseDto
{
    public Guid ExpenseId { get; init; }
    public Guid BusinessId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime ExpenseDate { get; init; }
    public string? Category { get; init; }
    public string? Vendor { get; init; }
    public bool IsTaxDeductible { get; init; }
    public string? Notes { get; init; }
}

public static class ExpenseExtensions
{
    public static ExpenseDto ToDto(this Expense expense)
    {
        return new ExpenseDto
        {
            ExpenseId = expense.ExpenseId,
            BusinessId = expense.BusinessId,
            Description = expense.Description,
            Amount = expense.Amount,
            ExpenseDate = expense.ExpenseDate,
            Category = expense.Category,
            Vendor = expense.Vendor,
            IsTaxDeductible = expense.IsTaxDeductible,
            Notes = expense.Notes,
        };
    }
}
