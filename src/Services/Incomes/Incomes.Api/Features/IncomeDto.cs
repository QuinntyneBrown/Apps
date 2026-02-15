namespace Incomes.Api.Features;

public record IncomeDto(
    Guid IncomeId,
    Guid TenantId,
    Guid UserId,
    Guid BusinessId,
    string Description,
    decimal Amount,
    string? Source,
    DateTime IncomeDate,
    DateTime CreatedAt);
