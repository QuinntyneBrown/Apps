using Expenses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Api.Features;

public record GetExpensesQuery(Guid TenantId, Guid BusinessId) : IRequest<IEnumerable<ExpenseDto>>;

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IEnumerable<ExpenseDto>>
{
    private readonly IExpensesDbContext _context;

    public GetExpensesQueryHandler(IExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Expenses
            .Where(e => e.TenantId == request.TenantId && e.BusinessId == request.BusinessId)
            .Select(e => new ExpenseDto(e.ExpenseId, e.TenantId, e.UserId, e.BusinessId, e.Description, e.Amount, e.Category, e.ExpenseDate, e.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
