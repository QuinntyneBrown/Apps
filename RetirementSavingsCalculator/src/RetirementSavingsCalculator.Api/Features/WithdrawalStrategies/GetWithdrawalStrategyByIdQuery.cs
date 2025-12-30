using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

public record GetWithdrawalStrategyByIdQuery : IRequest<WithdrawalStrategyDto?>
{
    public Guid WithdrawalStrategyId { get; init; }
}

public class GetWithdrawalStrategyByIdQueryHandler : IRequestHandler<GetWithdrawalStrategyByIdQuery, WithdrawalStrategyDto?>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<GetWithdrawalStrategyByIdQueryHandler> _logger;

    public GetWithdrawalStrategyByIdQueryHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<GetWithdrawalStrategyByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WithdrawalStrategyDto?> Handle(GetWithdrawalStrategyByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting withdrawal strategy {WithdrawalStrategyId}", request.WithdrawalStrategyId);

        var strategy = await _context.WithdrawalStrategies
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.WithdrawalStrategyId == request.WithdrawalStrategyId, cancellationToken);

        if (strategy == null)
        {
            _logger.LogWarning("Withdrawal strategy {WithdrawalStrategyId} not found", request.WithdrawalStrategyId);
            return null;
        }

        return strategy.ToDto();
    }
}
