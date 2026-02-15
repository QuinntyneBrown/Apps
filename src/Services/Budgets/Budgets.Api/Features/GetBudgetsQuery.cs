using Budgets.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgets.Api.Features;

public record GetBudgetsQuery : IRequest<IEnumerable<BudgetDto>>;

public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, IEnumerable<BudgetDto>>
{
    private readonly IBudgetsDbContext _context;

    public GetBudgetsQueryHandler(IBudgetsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        return await _context.VacationBudgets
            .Select(b => new BudgetDto
            {
                VacationBudgetId = b.VacationBudgetId,
                TripId = b.TripId,
                Category = b.Category,
                AllocatedAmount = b.AllocatedAmount,
                SpentAmount = b.SpentAmount,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
