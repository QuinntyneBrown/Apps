namespace Expenses.Api.Features;

public record ExpenseDto(
    Guid ExpenseId,
    Guid TenantId,
    Guid UserId,
    Guid BusinessId,
    string Description,
    decimal Amount,
    string? Category,
    DateTime ExpenseDate,
    DateTime CreatedAt);
