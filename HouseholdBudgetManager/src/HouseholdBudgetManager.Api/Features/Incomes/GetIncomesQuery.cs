// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Query to get incomes by budget ID.
/// </summary>
public record GetIncomesQuery : IRequest<IEnumerable<IncomeDto>>
{
    /// <summary>
    /// Gets or sets the budget ID filter.
    /// </summary>
    public Guid? BudgetId { get; init; }
}

/// <summary>
/// Handler for GetIncomesQuery.
/// </summary>
public class GetIncomesQueryHandler : IRequestHandler<GetIncomesQuery, IEnumerable<IncomeDto>>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<GetIncomesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIncomesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetIncomesQueryHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<GetIncomesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<IncomeDto>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting incomes for budget {BudgetId}", request.BudgetId);

        var query = _context.Incomes.AsNoTracking();

        if (request.BudgetId.HasValue)
        {
            query = query.Where(x => x.BudgetId == request.BudgetId.Value);
        }

        var incomes = await query
            .OrderByDescending(x => x.IncomeDate)
            .ToListAsync(cancellationToken);

        return incomes.Select(x => x.ToDto());
    }
}
