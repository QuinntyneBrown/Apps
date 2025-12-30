using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Expenses;

public record GetExpensesQuery : IRequest<IEnumerable<ExpenseDto>>
{
    public Guid? BusinessId { get; init; }
    public bool? IsTaxDeductible { get; init; }
    public string? Category { get; init; }
}

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IEnumerable<ExpenseDto>>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetExpensesQueryHandler> _logger;

    public GetExpensesQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetExpensesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting expenses for business {BusinessId}", request.BusinessId);

        var query = _context.Expenses.AsNoTracking();

        if (request.BusinessId.HasValue)
        {
            query = query.Where(e => e.BusinessId == request.BusinessId.Value);
        }

        if (request.IsTaxDeductible.HasValue)
        {
            query = query.Where(e => e.IsTaxDeductible == request.IsTaxDeductible.Value);
        }

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(e => e.Category == request.Category);
        }

        var expenses = await query
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync(cancellationToken);

        return expenses.Select(e => e.ToDto());
    }
}
