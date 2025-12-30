using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Api.Features.Expenses;

public record ExpenseDto
{
    public Guid ExpenseId { get; init; }
    public Guid PropertyId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string Category { get; init; } = string.Empty;
    public bool IsRecurring { get; init; }
    public string? Notes { get; init; }
}

public static class ExpenseExtensions
{
    public static ExpenseDto ToDto(this Expense expense)
    {
        return new ExpenseDto
        {
            ExpenseId = expense.ExpenseId,
            PropertyId = expense.PropertyId,
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            Category = expense.Category,
            IsRecurring = expense.IsRecurring,
            Notes = expense.Notes,
        };
    }
}
