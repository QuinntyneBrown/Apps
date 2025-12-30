// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Query to get a budget by ID.
/// </summary>
public record GetBudgetByIdQuery : IRequest<BudgetDto?>
{
    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }
}

/// <summary>
/// Handler for GetBudgetByIdQuery.
/// </summary>
public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto?>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<GetBudgetByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetBudgetByIdQueryHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<GetBudgetByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<BudgetDto?> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting budget {BudgetId}",
            request.BudgetId);

        var budget = await _context.Budgets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BudgetId == request.BudgetId, cancellationToken);

        if (budget == null)
        {
            _logger.LogInformation(
                "Budget {BudgetId} not found",
                request.BudgetId);
            return null;
        }

        return budget.ToDto();
    }
}
