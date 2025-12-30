// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Query to get an expense by ID.
/// </summary>
public record GetExpenseByIdQuery : IRequest<ExpenseDto?>
{
    /// <summary>
    /// Gets or sets the expense ID.
    /// </summary>
    public Guid ExpenseId { get; init; }
}

/// <summary>
/// Handler for GetExpenseByIdQuery.
/// </summary>
public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseDto?>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<GetExpenseByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpenseByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetExpenseByIdQueryHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<GetExpenseByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ExpenseDto?> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting expense {ExpenseId}",
            request.ExpenseId);

        var expense = await _context.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExpenseId == request.ExpenseId, cancellationToken);

        if (expense == null)
        {
            _logger.LogInformation(
                "Expense {ExpenseId} not found",
                request.ExpenseId);
            return null;
        }

        return expense.ToDto();
    }
}
