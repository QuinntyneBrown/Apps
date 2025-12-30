using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

public record GetWithdrawalStrategiesQuery : IRequest<IEnumerable<WithdrawalStrategyDto>>
{
    public Guid? RetirementScenarioId { get; init; }
    public WithdrawalStrategyType? StrategyType { get; init; }
    public bool? AdjustForInflation { get; init; }
    public decimal? MinWithdrawalRate { get; init; }
    public decimal? MaxWithdrawalRate { get; init; }
}

public class GetWithdrawalStrategiesQueryHandler : IRequestHandler<GetWithdrawalStrategiesQuery, IEnumerable<WithdrawalStrategyDto>>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<GetWithdrawalStrategiesQueryHandler> _logger;

    public GetWithdrawalStrategiesQueryHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<GetWithdrawalStrategiesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WithdrawalStrategyDto>> Handle(GetWithdrawalStrategiesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting withdrawal strategies for scenario {RetirementScenarioId}", request.RetirementScenarioId);

        var query = _context.WithdrawalStrategies.AsNoTracking();

        if (request.RetirementScenarioId.HasValue)
        {
            query = query.Where(s => s.RetirementScenarioId == request.RetirementScenarioId.Value);
        }

        if (request.StrategyType.HasValue)
        {
            query = query.Where(s => s.StrategyType == request.StrategyType.Value);
        }

        if (request.AdjustForInflation.HasValue)
        {
            query = query.Where(s => s.AdjustForInflation == request.AdjustForInflation.Value);
        }

        if (request.MinWithdrawalRate.HasValue)
        {
            query = query.Where(s => s.WithdrawalRate >= request.MinWithdrawalRate.Value);
        }

        if (request.MaxWithdrawalRate.HasValue)
        {
            query = query.Where(s => s.WithdrawalRate <= request.MaxWithdrawalRate.Value);
        }

        var strategies = await query
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);

        return strategies.Select(s => s.ToDto());
    }
}
