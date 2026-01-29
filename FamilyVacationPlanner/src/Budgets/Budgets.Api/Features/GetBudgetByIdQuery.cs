using Budgets.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgets.Api.Features;

public record GetBudgetByIdQuery(Guid VacationBudgetId) : IRequest<BudgetDto?>;

public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto?>
{
    private readonly IBudgetsDbContext _context;

    public GetBudgetByIdQueryHandler(IBudgetsDbContext context)
    {
        _context = context;
    }

    public async Task<BudgetDto?> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await _context.VacationBudgets
            .FirstOrDefaultAsync(b => b.VacationBudgetId == request.VacationBudgetId, cancellationToken);

        if (budget == null) return null;

        return new BudgetDto
        {
            VacationBudgetId = budget.VacationBudgetId,
            TripId = budget.TripId,
            Category = budget.Category,
            AllocatedAmount = budget.AllocatedAmount,
            SpentAmount = budget.SpentAmount,
            CreatedAt = budget.CreatedAt
        };
    }
}
