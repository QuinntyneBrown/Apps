using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.CashFlows;

public record UpdateCashFlowCommand : IRequest<CashFlowDto?>
{
    public Guid CashFlowId { get; init; }
    public Guid PropertyId { get; init; }
    public DateTime Date { get; init; }
    public decimal Income { get; init; }
    public decimal Expenses { get; init; }
    public string? Notes { get; init; }
}

public class UpdateCashFlowCommandHandler : IRequestHandler<UpdateCashFlowCommand, CashFlowDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<UpdateCashFlowCommandHandler> _logger;

    public UpdateCashFlowCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<UpdateCashFlowCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CashFlowDto?> Handle(UpdateCashFlowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating cash flow {CashFlowId}", request.CashFlowId);

        var cashFlow = await _context.CashFlows
            .FirstOrDefaultAsync(cf => cf.CashFlowId == request.CashFlowId, cancellationToken);

        if (cashFlow == null)
        {
            _logger.LogWarning("Cash flow {CashFlowId} not found", request.CashFlowId);
            return null;
        }

        cashFlow.PropertyId = request.PropertyId;
        cashFlow.Date = request.Date;
        cashFlow.Income = request.Income;
        cashFlow.Expenses = request.Expenses;
        cashFlow.Notes = request.Notes;
        cashFlow.CalculateNetCashFlow();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated cash flow {CashFlowId}", request.CashFlowId);

        return cashFlow.ToDto();
    }
}
