// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to create a new budget.
/// </summary>
public record CreateBudgetCommand : IRequest<BudgetDto>
{
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
/// Handler for CreateBudgetCommand.
/// </summary>
public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<CreateBudgetCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBudgetCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateBudgetCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<CreateBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating budget {Name} for period {Period}",
            request.Name,
            request.Period);

        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            Name = request.Name,
            Period = request.Period,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalIncome = request.TotalIncome,
            TotalExpenses = request.TotalExpenses,
            Status = request.Status,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created budget {BudgetId}",
            budget.BudgetId);

        return budget.ToDto();
    }
}
