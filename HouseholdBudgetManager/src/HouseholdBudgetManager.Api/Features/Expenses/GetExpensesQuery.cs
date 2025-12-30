// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Query to get expenses by budget ID.
/// </summary>
public record GetExpensesQuery : IRequest<IEnumerable<ExpenseDto>>
{
    /// <summary>
    /// Gets or sets the budget ID filter.
    /// </summary>
    public Guid? BudgetId { get; init; }
}

/// <summary>
/// Handler for GetExpensesQuery.
/// </summary>
public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IEnumerable<ExpenseDto>>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<GetExpensesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpensesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetExpensesQueryHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<GetExpensesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting expenses for budget {BudgetId}", request.BudgetId);

        var query = _context.Expenses.AsNoTracking();

        if (request.BudgetId.HasValue)
        {
            query = query.Where(x => x.BudgetId == request.BudgetId.Value);
        }

        var expenses = await query
            .OrderByDescending(x => x.ExpenseDate)
            .ToListAsync(cancellationToken);

        return expenses.Select(x => x.ToDto());
    }
}
