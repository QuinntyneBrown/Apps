using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Expenses;

public record GetExpensesQuery : IRequest<IEnumerable<ExpenseDto>>
{
    public Guid? PropertyId { get; init; }
    public string? Category { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool? RecurringOnly { get; init; }
}

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IEnumerable<ExpenseDto>>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetExpensesQueryHandler> _logger;

    public GetExpensesQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetExpensesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting expenses for property {PropertyId}", request.PropertyId);

        var query = _context.Expenses.AsNoTracking();

        if (request.PropertyId.HasValue)
        {
            query = query.Where(e => e.PropertyId == request.PropertyId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            query = query.Where(e => e.Category == request.Category);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(e => e.Date >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(e => e.Date <= request.EndDate.Value);
        }

        if (request.RecurringOnly == true)
        {
            query = query.Where(e => e.IsRecurring);
        }

        var expenses = await query
            .OrderByDescending(e => e.Date)
            .ToListAsync(cancellationToken);

        return expenses.Select(e => e.ToDto());
    }
}
