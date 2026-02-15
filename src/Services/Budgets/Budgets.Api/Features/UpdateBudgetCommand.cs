using Budgets.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgets.Api.Features;

public record UpdateBudgetCommand : IRequest<BudgetDto?>
{
    public Guid VacationBudgetId { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal AllocatedAmount { get; init; }
    public decimal? SpentAmount { get; init; }
}

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetDto?>
{
    private readonly IBudgetsDbContext _context;

    public UpdateBudgetCommandHandler(IBudgetsDbContext context)
    {
        _context = context;
    }

    public async Task<BudgetDto?> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.VacationBudgets
            .FirstOrDefaultAsync(b => b.VacationBudgetId == request.VacationBudgetId, cancellationToken);

        if (budget == null) return null;

        budget.Category = request.Category;
        budget.AllocatedAmount = request.AllocatedAmount;
        budget.SpentAmount = request.SpentAmount;

        await _context.SaveChangesAsync(cancellationToken);

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
