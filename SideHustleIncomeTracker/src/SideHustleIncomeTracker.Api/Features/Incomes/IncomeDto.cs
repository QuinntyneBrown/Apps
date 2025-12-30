using SideHustleIncomeTracker.Core;

namespace SideHustleIncomeTracker.Api.Features.Incomes;

public record IncomeDto
{
    public Guid IncomeId { get; init; }
    public Guid BusinessId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime IncomeDate { get; init; }
    public string? Client { get; init; }
    public string? InvoiceNumber { get; init; }
    public bool IsPaid { get; init; }
    public string? Notes { get; init; }
}

public static class IncomeExtensions
{
    public static IncomeDto ToDto(this Income income)
    {
        return new IncomeDto
        {
            IncomeId = income.IncomeId,
            BusinessId = income.BusinessId,
            Description = income.Description,
            Amount = income.Amount,
            IncomeDate = income.IncomeDate,
            Client = income.Client,
            InvoiceNumber = income.InvoiceNumber,
            IsPaid = income.IsPaid,
            Notes = income.Notes,
        };
    }
}
