using Strategies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Strategies.Api.Features;

public record GetStrategiesQuery : IRequest<IEnumerable<StrategyDto>>;

public class GetStrategiesQueryHandler : IRequestHandler<GetStrategiesQuery, IEnumerable<StrategyDto>>
{
    private readonly IStrategiesDbContext _context;
    public GetStrategiesQueryHandler(IStrategiesDbContext context) => _context = context;
    public async Task<IEnumerable<StrategyDto>> Handle(GetStrategiesQuery request, CancellationToken ct) => await _context.WithdrawalStrategies.AsNoTracking().Select(s => s.ToDto()).ToListAsync(ct);
}
