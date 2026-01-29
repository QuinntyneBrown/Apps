namespace TaxEstimates.Api.Features;

public record TaxEstimateDto(
    Guid TaxEstimateId,
    Guid TenantId,
    Guid UserId,
    Guid? BusinessId,
    int Year,
    int Quarter,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal NetIncome,
    decimal EstimatedTax,
    decimal TaxRate,
    DateTime CalculatedAt);
