// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Query to get an income by ID.
/// </summary>
public record GetIncomeByIdQuery : IRequest<IncomeDto?>
{
    /// <summary>
    /// Gets or sets the income ID.
    /// </summary>
    public Guid IncomeId { get; init; }
}

/// <summary>
/// Handler for GetIncomeByIdQuery.
/// </summary>
public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, IncomeDto?>
{
    private readonly IHouseholdBudgetManagerContext _context;
    private readonly ILogger<GetIncomeByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetIncomeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetIncomeByIdQueryHandler(
        IHouseholdBudgetManagerContext context,
        ILogger<GetIncomeByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IncomeDto?> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting income {IncomeId}",
            request.IncomeId);

        var income = await _context.Incomes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IncomeId == request.IncomeId, cancellationToken);

        if (income == null)
        {
            _logger.LogInformation(
                "Income {IncomeId} not found",
                request.IncomeId);
            return null;
        }

        return income.ToDto();
    }
}
