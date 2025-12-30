// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Api.Features.Plans;

/// <summary>
/// Data transfer object for Plan entity.
/// </summary>
public class PlanDto
{
    public Guid PlanId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime OpenedDate { get; set; }
    public string? Administrator { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Data transfer object for creating a new Plan.
/// </summary>
public class CreatePlanDto
{
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime OpenedDate { get; set; }
    public string? Administrator { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Data transfer object for updating an existing Plan.
/// </summary>
public class UpdatePlanDto
{
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public string? Administrator { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}
