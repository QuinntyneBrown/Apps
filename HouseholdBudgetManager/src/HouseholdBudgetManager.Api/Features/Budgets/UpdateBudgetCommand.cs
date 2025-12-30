// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to update an existing budget.
/// </summary>
public record UpdateBudgetCommand : IRequest<BudgetDto?>
{
    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }

    /// <summary>
    /// Gets or sets the name of the budget.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the budget period.
    /// </summary>
    public string Period { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets or sets the total income.
    /// </summary>
    public decimal TotalIncome { get; init; }

    /// <summary>
    /// Gets or sets the total expenses.
    /// </summary>
    public decimal TotalExpenses { get; init; }

    /// <summary>
    /// Gets or sets the budget status.
    /// </summary>
    public BudgetStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateBudgetCommand.
/// </summary>
public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetDto?>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<UpdateBudgetCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBudgetCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateBudgetCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<UpdateBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<BudgetDto?> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating budget {BudgetId}",
            request.BudgetId);

        var budget = await _context.Budgets
            .FirstOrDefaultAsync(x => x.BudgetId == request.BudgetId, cancellationToken);

        if (budget == null)
        {
            _logger.LogWarning(
                "Budget {BudgetId} not found",
                request.BudgetId);
            return null;
        }

        budget.Name = request.Name;
        budget.Period = request.Period;
        budget.StartDate = request.StartDate;
        budget.EndDate = request.EndDate;
        budget.TotalIncome = request.TotalIncome;
        budget.TotalExpenses = request.TotalExpenses;
        budget.Status = request.Status;
        budget.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated budget {BudgetId}",
            request.BudgetId);

        return budget.ToDto();
    }
}
