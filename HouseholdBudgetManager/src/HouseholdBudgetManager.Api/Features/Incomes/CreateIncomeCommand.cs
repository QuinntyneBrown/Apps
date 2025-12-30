// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to create a new income.
/// </summary>
public record CreateIncomeCommand : IRequest<IncomeDto>
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
    /// Gets or sets the source.
    /// </summary>
    public string Source { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the income date.
    /// </summary>
    public DateTime IncomeDate { get; init; }

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
/// Handler for CreateIncomeCommand.
/// </summary>
public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, IncomeDto>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<CreateIncomeCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateIncomeCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateIncomeCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<CreateIncomeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IncomeDto> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating income {Description} for budget {BudgetId}",
            request.Description,
            request.BudgetId);

        var income = new Income
        {
            IncomeId = Guid.NewGuid(),
            BudgetId = request.BudgetId,
            Description = request.Description,
            Amount = request.Amount,
            Source = request.Source,
            IncomeDate = request.IncomeDate,
            Notes = request.Notes,
            IsRecurring = request.IsRecurring,
        };

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created income {IncomeId}",
            income.IncomeId);

        return income.ToDto();
    }
}
