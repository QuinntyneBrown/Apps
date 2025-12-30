// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to update an existing income.
/// </summary>
public record UpdateIncomeCommand : IRequest<IncomeDto?>
{
    /// <summary>
    /// Gets or sets the income ID.
    /// </summary>
    public Guid IncomeId { get; init; }

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
/// Handler for UpdateIncomeCommand.
/// </summary>
public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, IncomeDto?>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<UpdateIncomeCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateIncomeCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateIncomeCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<UpdateIncomeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IncomeDto?> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating income {IncomeId}",
            request.IncomeId);

        var income = await _context.Incomes
            .FirstOrDefaultAsync(x => x.IncomeId == request.IncomeId, cancellationToken);

        if (income == null)
        {
            _logger.LogWarning(
                "Income {IncomeId} not found",
                request.IncomeId);
            return null;
        }

        income.Description = request.Description;
        income.Amount = request.Amount;
        income.Source = request.Source;
        income.IncomeDate = request.IncomeDate;
        income.Notes = request.Notes;
        income.IsRecurring = request.IsRecurring;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated income {IncomeId}",
            request.IncomeId);

        return income.ToDto();
    }
}
