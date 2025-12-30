using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.VacationBudgets;

public record GetVacationBudgetsQuery : IRequest<IEnumerable<VacationBudgetDto>>
{
    public Guid? TripId { get; init; }
}

public class GetVacationBudgetsQueryHandler : IRequestHandler<GetVacationBudgetsQuery, IEnumerable<VacationBudgetDto>>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetVacationBudgetsQueryHandler> _logger;

    public GetVacationBudgetsQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetVacationBudgetsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<VacationBudgetDto>> Handle(GetVacationBudgetsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vacation budgets for trip {TripId}", request.TripId);

        var query = _context.VacationBudgets.AsNoTracking();

        if (request.TripId.HasValue)
        {
            query = query.Where(vb => vb.TripId == request.TripId.Value);
        }

        var budgets = await query
            .OrderByDescending(vb => vb.CreatedAt)
            .ToListAsync(cancellationToken);

        return budgets.Select(vb => vb.ToDto());
    }
}
