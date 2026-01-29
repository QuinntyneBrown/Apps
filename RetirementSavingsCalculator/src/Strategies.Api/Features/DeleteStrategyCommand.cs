using Strategies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Strategies.Api.Features;

public record DeleteStrategyCommand(Guid StrategyId) : IRequest<bool>;

public class DeleteStrategyCommandHandler : IRequestHandler<DeleteStrategyCommand, bool>
{
    private readonly IStrategiesDbContext _context;
    public DeleteStrategyCommandHandler(IStrategiesDbContext context) => _context = context;
    public async Task<bool> Handle(DeleteStrategyCommand request, CancellationToken ct)
    {
        var strategy = await _context.WithdrawalStrategies.FirstOrDefaultAsync(s => s.WithdrawalStrategyId == request.StrategyId, ct);
        if (strategy == null) return false;
        _context.WithdrawalStrategies.Remove(strategy);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
