using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

public record DeleteWithdrawalStrategyCommand : IRequest<bool>
{
    public Guid WithdrawalStrategyId { get; init; }
}

public class DeleteWithdrawalStrategyCommandHandler : IRequestHandler<DeleteWithdrawalStrategyCommand, bool>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<DeleteWithdrawalStrategyCommandHandler> _logger;

    public DeleteWithdrawalStrategyCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<DeleteWithdrawalStrategyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteWithdrawalStrategyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting withdrawal strategy {WithdrawalStrategyId}", request.WithdrawalStrategyId);

        var strategy = await _context.WithdrawalStrategies
            .FirstOrDefaultAsync(s => s.WithdrawalStrategyId == request.WithdrawalStrategyId, cancellationToken);

        if (strategy == null)
        {
            _logger.LogWarning("Withdrawal strategy {WithdrawalStrategyId} not found", request.WithdrawalStrategyId);
            return false;
        }

        _context.WithdrawalStrategies.Remove(strategy);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted withdrawal strategy {WithdrawalStrategyId}", request.WithdrawalStrategyId);

        return true;
    }
}
