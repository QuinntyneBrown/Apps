// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Command to delete an income.
/// </summary>
public record DeleteIncomeCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the income ID.
    /// </summary>
    public Guid IncomeId { get; init; }
}

/// <summary>
/// Handler for DeleteIncomeCommand.
/// </summary>
public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<DeleteIncomeCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIncomeCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteIncomeCommandHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<DeleteIncomeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting income {IncomeId}",
            request.IncomeId);

        var income = await _context.Incomes
            .FirstOrDefaultAsync(x => x.IncomeId == request.IncomeId, cancellationToken);

        if (income == null)
        {
            _logger.LogWarning(
                "Income {IncomeId} not found",
                request.IncomeId);
            return false;
        }

        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted income {IncomeId}",
            request.IncomeId);

        return true;
    }
}
