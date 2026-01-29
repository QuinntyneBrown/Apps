using Budgets.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgets.Api.Features;

public record DeleteBudgetCommand(Guid VacationBudgetId) : IRequest<bool>;

public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, bool>
{
    private readonly IBudgetsDbContext _context;

    public DeleteBudgetCommandHandler(IBudgetsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.VacationBudgets
            .FirstOrDefaultAsync(b => b.VacationBudgetId == request.VacationBudgetId, cancellationToken);

        if (budget == null) return false;

        _context.VacationBudgets.Remove(budget);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
