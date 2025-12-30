// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Query to get all budgets for a project.
/// </summary>
public record GetBudgetsByProjectIdQuery : IRequest<IEnumerable<BudgetDto>>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for GetBudgetsByProjectIdQuery.
/// </summary>
public class GetBudgetsByProjectIdQueryHandler : IRequestHandler<GetBudgetsByProjectIdQuery, IEnumerable<BudgetDto>>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<GetBudgetsByProjectIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetsByProjectIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetBudgetsByProjectIdQueryHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<GetBudgetsByProjectIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BudgetDto>> Handle(GetBudgetsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting budgets for project {ProjectId}",
            request.ProjectId);

        var budgets = await _context.Budgets
            .AsNoTracking()
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return budgets.Select(b => b.ToDto());
    }
}
