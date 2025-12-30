// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to create a new expense.
/// </summary>
public record CreateExpenseCommand : IRequest<ExpenseDto>
{
    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }

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
/// Handler for CreateExpenseCommand.
/// </summary>
public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDto>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<CreateExpenseCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateExpenseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateExpenseCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<CreateExpenseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating expense {Description} for budget {BudgetId}",
            request.Description,
            request.BudgetId);

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            BudgetId = request.BudgetId,
            Description = request.Description,
            Amount = request.Amount,
            Category = request.Category,
            ExpenseDate = request.ExpenseDate,
            Payee = request.Payee,
            PaymentMethod = request.PaymentMethod,
            Notes = request.Notes,
            IsRecurring = request.IsRecurring,
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created expense {ExpenseId}",
            expense.ExpenseId);

        return expense.ToDto();
    }
}
