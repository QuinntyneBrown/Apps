using Expenses.Core;
using Expenses.Core.Models;
using MediatR;

namespace Expenses.Api.Features;

public record CreateExpenseCommand(Guid TenantId, Guid UserId, Guid BusinessId, string Description, decimal Amount, DateTime ExpenseDate, string? Category) : IRequest<ExpenseDto>;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDto>
{
    private readonly IExpensesDbContext _context;

    public CreateExpenseCommandHandler(IExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = new Expense(request.TenantId, request.UserId, request.BusinessId, request.Description, request.Amount, request.ExpenseDate, request.Category);

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return new ExpenseDto(expense.ExpenseId, expense.TenantId, expense.UserId, expense.BusinessId, expense.Description, expense.Amount, expense.Category, expense.ExpenseDate, expense.CreatedAt);
    }
}
