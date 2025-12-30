using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Expenses;

public record GetExpenseByIdQuery : IRequest<ExpenseDto?>
{
    public Guid ExpenseId { get; init; }
}

public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetExpenseByIdQueryHandler> _logger;

    public GetExpenseByIdQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetExpenseByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ExpenseDto?> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting expense {ExpenseId}", request.ExpenseId);

        var expense = await _context.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ExpenseId == request.ExpenseId, cancellationToken);

        return expense?.ToDto();
    }
}
