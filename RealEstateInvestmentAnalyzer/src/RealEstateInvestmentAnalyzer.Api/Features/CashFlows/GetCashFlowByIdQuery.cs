using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.CashFlows;

public record GetCashFlowByIdQuery : IRequest<CashFlowDto?>
{
    public Guid CashFlowId { get; init; }
}

public class GetCashFlowByIdQueryHandler : IRequestHandler<GetCashFlowByIdQuery, CashFlowDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetCashFlowByIdQueryHandler> _logger;

    public GetCashFlowByIdQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetCashFlowByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CashFlowDto?> Handle(GetCashFlowByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting cash flow {CashFlowId}", request.CashFlowId);

        var cashFlow = await _context.CashFlows
            .AsNoTracking()
            .FirstOrDefaultAsync(cf => cf.CashFlowId == request.CashFlowId, cancellationToken);

        return cashFlow?.ToDto();
    }
}
