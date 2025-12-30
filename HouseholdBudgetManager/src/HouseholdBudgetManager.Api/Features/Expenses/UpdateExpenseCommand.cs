// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to update an existing expense.
/// </summary>
public record UpdateExpenseCommand : IRequest<ExpenseDto?>
{
    /// <summary>
    /// Gets or sets the expense ID.
    /// </summary>
    public Guid ExpenseId { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public ExpenseCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the expense date.
    /// </summary>
    public DateTime ExpenseDate { get; init; }

    /// <summary>
    /// Gets or sets the payee.
    /// </summary>
    public string? Payee { get; init; }

    /// <summary>
    /// Gets or sets the payment method.
    /// </summary>
    public string? PaymentMethod { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets whether this is recurring.
    /// </summary>
    public bool IsRecurring { get; init; }
}

/// <summary>
/// Handler for UpdateExpenseCommand.
/// </summary>
public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ExpenseDto?>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<UpdateExpenseCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateExpenseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateExpenseCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<UpdateExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ExpenseDto?> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating expense {ExpenseId}",
            request.ExpenseId);

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(x => x.ExpenseId == request.ExpenseId, cancellationToken);

        if (expense == null)
        {
            _logger.LogWarning(
                "Expense {ExpenseId} not found",
                request.ExpenseId);
            return null;
        }

        expense.Description = request.Description;
        expense.Amount = request.Amount;
        expense.Category = request.Category;
        expense.ExpenseDate = request.ExpenseDate;
        expense.Payee = request.Payee;
        expense.PaymentMethod = request.PaymentMethod;
        expense.Notes = request.Notes;
        expense.IsRecurring = request.IsRecurring;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated expense {ExpenseId}",
            request.ExpenseId);

        return expense.ToDto();
    }
}
