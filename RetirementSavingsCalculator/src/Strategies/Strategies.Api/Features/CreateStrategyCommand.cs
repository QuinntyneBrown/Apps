using Strategies.Core;
using Strategies.Core.Models;
using MediatR;

namespace Strategies.Api.Features;

public record CreateStrategyCommand(Guid TenantId, Guid RetirementScenarioId, string Name, decimal WithdrawalRate, WithdrawalStrategyType StrategyType) : IRequest<StrategyDto>;

public class CreateStrategyCommandHandler : IRequestHandler<CreateStrategyCommand, StrategyDto>
{
    private readonly IStrategiesDbContext _context;
    public CreateStrategyCommandHandler(IStrategiesDbContext context) => _context = context;
    public async Task<StrategyDto> Handle(CreateStrategyCommand request, CancellationToken ct)
    {
        var strategy = new WithdrawalStrategy { WithdrawalStrategyId = Guid.NewGuid(), TenantId = request.TenantId, RetirementScenarioId = request.RetirementScenarioId, Name = request.Name, WithdrawalRate = request.WithdrawalRate, StrategyType = request.StrategyType };
        _context.WithdrawalStrategies.Add(strategy);
        await _context.SaveChangesAsync(ct);
        return strategy.ToDto();
    }
}
