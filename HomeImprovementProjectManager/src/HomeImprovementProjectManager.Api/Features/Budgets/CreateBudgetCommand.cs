// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Command to create a new budget.
/// </summary>
public record CreateBudgetCommand : IRequest<BudgetDto>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the allocated amount.
    /// </summary>
    public decimal AllocatedAmount { get; init; }

    /// <summary>
    /// Gets or sets the spent amount.
    /// </summary>
    public decimal? SpentAmount { get; init; }
}

/// <summary>
/// Handler for CreateBudgetCommand.
/// </summary>
public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<CreateBudgetCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBudgetCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateBudgetCommandHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<CreateBudgetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating budget for project {ProjectId}, category {Category}",
            request.ProjectId,
            request.Category);

        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            Category = request.Category,
            AllocatedAmount = request.AllocatedAmount,
            SpentAmount = request.SpentAmount,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created budget {BudgetId} for project {ProjectId}",
            budget.BudgetId,
            request.ProjectId);

        return budget.ToDto();
    }
}
