// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;

namespace CollegeSavingsPlanner.Api.Features.Projections;

/// <summary>
/// Command to create a new projection.
/// </summary>
public class CreateProjectionCommand : IRequest<ProjectionDto>
{
    public CreateProjectionDto Projection { get; set; } = new();
}

/// <summary>
/// Handler for CreateProjectionCommand.
/// </summary>
public class CreateProjectionCommandHandler : IRequestHandler<CreateProjectionCommand, ProjectionDto>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public CreateProjectionCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<ProjectionDto> Handle(CreateProjectionCommand request, CancellationToken cancellationToken)
    {
        var projection = new Projection
        {
            ProjectionId = Guid.NewGuid(),
            PlanId = request.Projection.PlanId,
            Name = request.Projection.Name,
            CurrentSavings = request.Projection.CurrentSavings,
            MonthlyContribution = request.Projection.MonthlyContribution,
            ExpectedReturnRate = request.Projection.ExpectedReturnRate,
            YearsUntilCollege = request.Projection.YearsUntilCollege,
            TargetGoal = request.Projection.TargetGoal,
            CreatedAt = DateTime.UtcNow
        };

        // Calculate projected balance
        projection.CalculateProjectedBalance();

        _context.Projections.Add(projection);
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
