// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Projections;

/// <summary>
/// Query to get all projections.
/// </summary>
public class GetProjectionsQuery : IRequest<List<ProjectionDto>>
{
    public Guid? PlanId { get; set; }
}

/// <summary>
/// Handler for GetProjectionsQuery.
/// </summary>
public class GetProjectionsQueryHandler : IRequestHandler<GetProjectionsQuery, List<ProjectionDto>>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetProjectionsQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectionDto>> Handle(GetProjectionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Projections.AsQueryable();

        if (request.PlanId.HasValue)
        {
            query = query.Where(p => p.PlanId == request.PlanId.Value);
        }

        var projections = await query
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
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return projections;
    }
}
