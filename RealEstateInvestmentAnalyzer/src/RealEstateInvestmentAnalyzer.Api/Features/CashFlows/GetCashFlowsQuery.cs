using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.CashFlows;

public record GetCashFlowsQuery : IRequest<IEnumerable<CashFlowDto>>
{
    public Guid? PropertyId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetCashFlowsQueryHandler : IRequestHandler<GetCashFlowsQuery, IEnumerable<CashFlowDto>>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetCashFlowsQueryHandler> _logger;

    public GetCashFlowsQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetCashFlowsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CashFlowDto>> Handle(GetCashFlowsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting cash flows for property {PropertyId}", request.PropertyId);

        var query = _context.CashFlows.AsNoTracking();

        if (request.PropertyId.HasValue)
        {
            query = query.Where(cf => cf.PropertyId == request.PropertyId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(cf => cf.Date >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(cf => cf.Date <= request.EndDate.Value);
        }

        var cashFlows = await query
            .OrderByDescending(cf => cf.Date)
            .ToListAsync(cancellationToken);

        return cashFlows.Select(cf => cf.ToDto());
    }
}
