using Expenses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Api.Features;

public record DeleteExpenseCommand(Guid ExpenseId, Guid TenantId) : IRequest<bool>;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
{
    private readonly IExpensesDbContext _context;

    public DeleteExpenseCommandHandler(IExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.ExpenseId == request.ExpenseId && e.TenantId == request.TenantId, cancellationToken);

        if (expense == null) return false;

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
