// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Projections;

/// <summary>
/// Query to get a projection by ID.
/// </summary>
public class GetProjectionByIdQuery : IRequest<ProjectionDto?>
{
    public Guid ProjectionId { get; set; }
}

/// <summary>
/// Handler for GetProjectionByIdQuery.
/// </summary>
public class GetProjectionByIdQueryHandler : IRequestHandler<GetProjectionByIdQuery, ProjectionDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetProjectionByIdQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<ProjectionDto?> Handle(GetProjectionByIdQuery request, CancellationToken cancellationToken)
    {
        var projection = await _context.Projections
            .Where(p => p.ProjectionId == request.ProjectionId)
            .Select(p => new ProjectionDto
            {
                ProjectionId = p.ProjectionId,
                PlanId = p.PlanId,
                Name = p.Name,
                CurrentSavings = p.CurrentSavings,
                MonthlyContribution = p.MonthlyContribution,
                ExpectedReturnRate = p.ExpectedReturnRate,
                YearsUntilCollege = p.YearsUntilCollege,
                TargetGoal = p.TargetGoal,
                ProjectedBalance = p.ProjectedBalance,
                CreatedAt = p.CreatedAt,
                GoalDifference = p.CalculateGoalDifference(),
                RequiredMonthlyContribution = p.CalculateRequiredMonthlyContribution()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return projection;
    }
}
