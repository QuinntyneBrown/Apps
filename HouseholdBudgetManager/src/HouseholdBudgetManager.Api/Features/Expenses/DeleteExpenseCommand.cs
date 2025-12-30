// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to delete an expense.
/// </summary>
public record DeleteExpenseCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the expense ID.
    /// </summary>
    public Guid ExpenseId { get; init; }
}

/// <summary>
/// Handler for DeleteExpenseCommand.
/// </summary>
public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<DeleteExpenseCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteExpenseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteExpenseCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<DeleteExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting expense {ExpenseId}",
            request.ExpenseId);

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(x => x.ExpenseId == request.ExpenseId, cancellationToken);

        if (expense == null)
        {
            _logger.LogWarning(
                "Expense {ExpenseId} not found",
                request.ExpenseId);
            return false;
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted expense {ExpenseId}",
            request.ExpenseId);

        return true;
    }
}
