using Expenses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Api.Features;

public record GetExpenseByIdQuery(Guid ExpenseId, Guid TenantId) : IRequest<ExpenseDto?>;

public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseDto?>
{
    private readonly IExpensesDbContext _context;

    public GetExpenseByIdQueryHandler(IExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseDto?> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(e => e.ExpenseId == request.ExpenseId && e.TenantId == request.TenantId, cancellationToken);

        return expense == null ? null : new ExpenseDto(expense.ExpenseId, expense.TenantId, expense.UserId, expense.BusinessId, expense.Description, expense.Amount, expense.Category, expense.ExpenseDate, expense.CreatedAt);
    }
}
