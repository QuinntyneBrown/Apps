// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Projections;

/// <summary>
/// Command to update an existing projection.
/// </summary>
public class UpdateProjectionCommand : IRequest<ProjectionDto?>
{
    public Guid ProjectionId { get; set; }
    public UpdateProjectionDto Projection { get; set; } = new();
}

/// <summary>
/// Handler for UpdateProjectionCommand.
/// </summary>
public class UpdateProjectionCommandHandler : IRequestHandler<UpdateProjectionCommand, ProjectionDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public UpdateProjectionCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<ProjectionDto?> Handle(UpdateProjectionCommand request, CancellationToken cancellationToken)
    {
        var projection = await _context.Projections
            .FirstOrDefaultAsync(p => p.ProjectionId == request.ProjectionId, cancellationToken);

        if (projection == null)
        {
            return null;
        }

        projection.Name = request.Projection.Name;
        projection.CurrentSavings = request.Projection.CurrentSavings;
        projection.MonthlyContribution = request.Projection.MonthlyContribution;
        projection.ExpectedReturnRate = request.Projection.ExpectedReturnRate;
        projection.YearsUntilCollege = request.Projection.YearsUntilCollege;
        projection.TargetGoal = request.Projection.TargetGoal;

        // Recalculate projected balance
        projection.CalculateProjectedBalance();

        await _context.SaveChangesAsync(cancellationToken);

        return new ProjectionDto
        {
            ProjectionId = projection.ProjectionId,
            PlanId = projection.PlanId,
            Name = projection.Name,
            CurrentSavings = projection.CurrentSavings,
            MonthlyContribution = projection.MonthlyContribution,
            ExpectedReturnRate = projection.ExpectedReturnRate,
            YearsUntilCollege = projection.YearsUntilCollege,
            TargetGoal = projection.TargetGoal,
            ProjectedBalance = projection.ProjectedBalance,
            CreatedAt = projection.CreatedAt,
            GoalDifference = projection.CalculateGoalDifference(),
            RequiredMonthlyContribution = projection.CalculateRequiredMonthlyContribution()
        };
    }
}
