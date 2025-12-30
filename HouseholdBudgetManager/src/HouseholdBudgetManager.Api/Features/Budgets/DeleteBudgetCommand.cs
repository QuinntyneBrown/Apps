// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to delete a budget.
/// </summary>
public record DeleteBudgetCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }
}

/// <summary>
/// Handler for DeleteBudgetCommand.
/// </summary>
public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, bool>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<DeleteBudgetCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBudgetCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteBudgetCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<DeleteBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting budget {BudgetId}",
            request.BudgetId);

        var budget = await _context.Budgets
            .FirstOrDefaultAsync(x => x.BudgetId == request.BudgetId, cancellationToken);

        if (budget == null)
        {
            _logger.LogWarning(
                "Budget {BudgetId} not found",
                request.BudgetId);
            return false;
        }

        _context.Budgets.Remove(budget);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted budget {BudgetId}",
            request.BudgetId);

        return true;
    }
}
