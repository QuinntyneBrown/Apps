using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Incomes;

public record GetIncomeByIdQuery : IRequest<IncomeDto?>
{
    public Guid IncomeId { get; init; }
}

public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, IncomeDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetIncomeByIdQueryHandler> _logger;

    public GetIncomeByIdQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetIncomeByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IncomeDto?> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting income {IncomeId}", request.IncomeId);

        var income = await _context.Incomes
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IncomeId == request.IncomeId, cancellationToken);

        return income?.ToDto();
    }
}
