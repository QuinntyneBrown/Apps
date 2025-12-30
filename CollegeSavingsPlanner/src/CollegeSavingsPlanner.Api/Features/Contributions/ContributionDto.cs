// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Api.Features.Contributions;

/// <summary>
/// Data transfer object for Contribution entity.
/// </summary>
public class ContributionDto
{
    public Guid ContributionId { get; set; }
    public Guid PlanId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string? Contributor { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
}

/// <summary>
/// Data transfer object for creating a new Contribution.
/// </summary>
public class CreateContributionDto
{
    public Guid PlanId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string? Contributor { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
}

/// <summary>
/// Data transfer object for updating an existing Contribution.
/// </summary>
public class UpdateContributionDto
{
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string? Contributor { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
}
