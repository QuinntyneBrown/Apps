using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Expenses;

public record DeleteExpenseCommand : IRequest<bool>
{
    public Guid ExpenseId { get; init; }
}

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<DeleteExpenseCommandHandler> _logger;

    public DeleteExpenseCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<DeleteExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting expense {ExpenseId}", request.ExpenseId);

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.ExpenseId == request.ExpenseId, cancellationToken);

        if (expense == null)
        {
            _logger.LogWarning("Expense {ExpenseId} not found", request.ExpenseId);
            return false;
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted expense {ExpenseId}", request.ExpenseId);

        return true;
    }
}
