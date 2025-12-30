using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

public record CreateWithdrawalStrategyCommand : IRequest<WithdrawalStrategyDto>
{
    public Guid RetirementScenarioId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal WithdrawalRate { get; init; }
    public decimal AnnualWithdrawalAmount { get; init; }
    public bool AdjustForInflation { get; init; }
    public decimal? MinimumBalance { get; init; }
    public WithdrawalStrategyType StrategyType { get; init; }
    public string? Notes { get; init; }
}

public class CreateWithdrawalStrategyCommandHandler : IRequestHandler<CreateWithdrawalStrategyCommand, WithdrawalStrategyDto>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<CreateWithdrawalStrategyCommandHandler> _logger;

    public CreateWithdrawalStrategyCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<CreateWithdrawalStrategyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WithdrawalStrategyDto> Handle(CreateWithdrawalStrategyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating withdrawal strategy '{Name}' for scenario {RetirementScenarioId}",
            request.Name,
            request.RetirementScenarioId);

        var strategy = new WithdrawalStrategy
        {
            WithdrawalStrategyId = Guid.NewGuid(),
            RetirementScenarioId = request.RetirementScenarioId,
            Name = request.Name,
            WithdrawalRate = request.WithdrawalRate,
            AnnualWithdrawalAmount = request.AnnualWithdrawalAmount,
            AdjustForInflation = request.AdjustForInflation,
            MinimumBalance = request.MinimumBalance,
            StrategyType = request.StrategyType,
            Notes = request.Notes,
        };

        strategy.Validate();

        _context.WithdrawalStrategies.Add(strategy);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created withdrawal strategy {WithdrawalStrategyId}",
            strategy.WithdrawalStrategyId);

        return strategy.ToDto();
    }
}
