using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Api.Features.CashFlows;

public record CashFlowDto
{
    public Guid CashFlowId { get; init; }
    public Guid PropertyId { get; init; }
    public DateTime Date { get; init; }
    public decimal Income { get; init; }
    public decimal Expenses { get; init; }
    public decimal NetCashFlow { get; init; }
    public string? Notes { get; init; }
}

public static class CashFlowExtensions
{
    public static CashFlowDto ToDto(this CashFlow cashFlow)
    {
        return new CashFlowDto
        {
            CashFlowId = cashFlow.CashFlowId,
            PropertyId = cashFlow.PropertyId,
            Date = cashFlow.Date,
            Income = cashFlow.Income,
            Expenses = cashFlow.Expenses,
            NetCashFlow = cashFlow.NetCashFlow,
            Notes = cashFlow.Notes,
        };
    }
}
