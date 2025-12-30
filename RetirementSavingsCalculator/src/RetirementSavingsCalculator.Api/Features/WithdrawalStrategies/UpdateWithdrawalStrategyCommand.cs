using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

public record UpdateWithdrawalStrategyCommand : IRequest<WithdrawalStrategyDto?>
{
    public Guid WithdrawalStrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal WithdrawalRate { get; init; }
    public decimal AnnualWithdrawalAmount { get; init; }
    public bool AdjustForInflation { get; init; }
    public decimal? MinimumBalance { get; init; }
    public WithdrawalStrategyType StrategyType { get; init; }
    public string? Notes { get; init; }
}

public class UpdateWithdrawalStrategyCommandHandler : IRequestHandler<UpdateWithdrawalStrategyCommand, WithdrawalStrategyDto?>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<UpdateWithdrawalStrategyCommandHandler> _logger;

    public UpdateWithdrawalStrategyCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<UpdateWithdrawalStrategyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WithdrawalStrategyDto?> Handle(UpdateWithdrawalStrategyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating withdrawal strategy {WithdrawalStrategyId}", request.WithdrawalStrategyId);

        var strategy = await _context.WithdrawalStrategies
            .FirstOrDefaultAsync(s => s.WithdrawalStrategyId == request.WithdrawalStrategyId, cancellationToken);

        if (strategy == null)
        {
            _logger.LogWarning("Withdrawal strategy {WithdrawalStrategyId} not found", request.WithdrawalStrategyId);
            return null;
        }

        strategy.Name = request.Name;
        strategy.WithdrawalRate = request.WithdrawalRate;
        strategy.AnnualWithdrawalAmount = request.AnnualWithdrawalAmount;
        strategy.AdjustForInflation = request.AdjustForInflation;
        strategy.MinimumBalance = request.MinimumBalance;
        strategy.StrategyType = request.StrategyType;
        strategy.Notes = request.Notes;

        strategy.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated withdrawal strategy {WithdrawalStrategyId}", request.WithdrawalStrategyId);

        return strategy.ToDto();
    }
}
