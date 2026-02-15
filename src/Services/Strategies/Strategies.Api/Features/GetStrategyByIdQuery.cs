using Strategies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Strategies.Api.Features;

public record GetStrategyByIdQuery(Guid StrategyId) : IRequest<StrategyDto?>;

public class GetStrategyByIdQueryHandler : IRequestHandler<GetStrategyByIdQuery, StrategyDto?>
{
    private readonly IStrategiesDbContext _context;
    public GetStrategyByIdQueryHandler(IStrategiesDbContext context) => _context = context;
    public async Task<StrategyDto?> Handle(GetStrategyByIdQuery request, CancellationToken ct)
    {
        var strategy = await _context.WithdrawalStrategies.AsNoTracking().FirstOrDefaultAsync(s => s.WithdrawalStrategyId == request.StrategyId, ct);
        return strategy?.ToDto();
    }
}
