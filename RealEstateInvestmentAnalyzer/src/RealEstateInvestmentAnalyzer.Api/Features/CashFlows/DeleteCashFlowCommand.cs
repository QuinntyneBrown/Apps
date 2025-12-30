using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.CashFlows;

public record DeleteCashFlowCommand : IRequest<bool>
{
    public Guid CashFlowId { get; init; }
}

public class DeleteCashFlowCommandHandler : IRequestHandler<DeleteCashFlowCommand, bool>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<DeleteCashFlowCommandHandler> _logger;

    public DeleteCashFlowCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<DeleteCashFlowCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCashFlowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting cash flow {CashFlowId}", request.CashFlowId);

        var cashFlow = await _context.CashFlows
            .FirstOrDefaultAsync(cf => cf.CashFlowId == request.CashFlowId, cancellationToken);

        if (cashFlow == null)
        {
            _logger.LogWarning("Cash flow {CashFlowId} not found", request.CashFlowId);
            return false;
        }

        _context.CashFlows.Remove(cashFlow);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted cash flow {CashFlowId}", request.CashFlowId);

        return true;
    }
}
