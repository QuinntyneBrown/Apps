using Expenses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Api.Features;

public record UpdateExpenseCommand(Guid ExpenseId, Guid TenantId, string? Description, decimal? Amount, DateTime? ExpenseDate, string? Category) : IRequest<ExpenseDto?>;

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ExpenseDto?>
{
    private readonly IExpensesDbContext _context;

    public UpdateExpenseCommandHandler(IExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseDto?> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.ExpenseId == request.ExpenseId && e.TenantId == request.TenantId, cancellationToken);

        if (expense == null) return null;

        expense.Update(request.Description, request.Amount, request.ExpenseDate, request.Category);
        await _context.SaveChangesAsync(cancellationToken);

        return new ExpenseDto(expense.ExpenseId, expense.TenantId, expense.UserId, expense.BusinessId, expense.Description, expense.Amount, expense.Category, expense.ExpenseDate, expense.CreatedAt);
    }
}
