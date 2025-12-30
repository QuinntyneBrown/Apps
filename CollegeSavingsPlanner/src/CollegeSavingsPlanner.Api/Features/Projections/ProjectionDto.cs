// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Api.Features.Projections;

/// <summary>
/// Data transfer object for Projection entity.
/// </summary>
public class ProjectionDto
{
    public Guid ProjectionId { get; set; }
    public Guid PlanId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CurrentSavings { get; set; }
    public decimal MonthlyContribution { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public int YearsUntilCollege { get; set; }
    public decimal TargetGoal { get; set; }
    public decimal ProjectedBalance { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal GoalDifference { get; set; }
    public decimal RequiredMonthlyContribution { get; set; }
}

/// <summary>
/// Data transfer object for creating a new Projection.
/// </summary>
public class CreateProjectionDto
{
    public Guid PlanId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CurrentSavings { get; set; }
    public decimal MonthlyContribution { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public int YearsUntilCollege { get; set; }
    public decimal TargetGoal { get; set; }
}

/// <summary>
/// Data transfer object for updating an existing Projection.
/// </summary>
public class UpdateProjectionDto
{
    public string Name { get; set; } = string.Empty;
    public decimal CurrentSavings { get; set; }
    public decimal MonthlyContribution { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public int YearsUntilCollege { get; set; }
    public decimal TargetGoal { get; set; }
}
