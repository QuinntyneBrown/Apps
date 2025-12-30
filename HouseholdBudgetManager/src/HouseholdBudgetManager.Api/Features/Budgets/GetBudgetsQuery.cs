// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Query to get all budgets.
/// </summary>
public record GetBudgetsQuery : IRequest<IEnumerable<BudgetDto>>
{
}

/// <summary>
/// Handler for GetBudgetsQuery.
/// </summary>
public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, IEnumerable<BudgetDto>>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<GetBudgetsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetBudgetsQueryHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<GetBudgetsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all budgets");

        var budgets = await _context.Budgets
            .AsNoTracking()
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(cancellationToken);

        return budgets.Select(x => x.ToDto());
    }
}
