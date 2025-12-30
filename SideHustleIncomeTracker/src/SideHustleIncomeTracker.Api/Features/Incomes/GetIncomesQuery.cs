using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Incomes;

public record GetIncomesQuery : IRequest<IEnumerable<IncomeDto>>
{
    public Guid? BusinessId { get; init; }
    public bool? IsPaid { get; init; }
}

public class GetIncomesQueryHandler : IRequestHandler<GetIncomesQuery, IEnumerable<IncomeDto>>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetIncomesQueryHandler> _logger;

    public GetIncomesQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetIncomesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<IncomeDto>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting incomes for business {BusinessId}", request.BusinessId);

        var query = _context.Incomes.AsNoTracking();

        if (request.BusinessId.HasValue)
        {
            query = query.Where(i => i.BusinessId == request.BusinessId.Value);
        }

        if (request.IsPaid.HasValue)
        {
            query = query.Where(i => i.IsPaid == request.IsPaid.Value);
        }

        var incomes = await query
            .OrderByDescending(i => i.IncomeDate)
            .ToListAsync(cancellationToken);

        return incomes.Select(i => i.ToDto());
    }
}
