// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Api.Features.Beneficiaries;

/// <summary>
/// Data transfer object for Beneficiary entity.
/// </summary>
public class BeneficiaryDto
{
    public Guid BeneficiaryId { get; set; }
    public Guid PlanId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Relationship { get; set; }
    public int? ExpectedCollegeStartYear { get; set; }
    public bool IsPrimary { get; set; }
    public int Age { get; set; }
    public int? YearsUntilCollege { get; set; }
}

/// <summary>
/// Data transfer object for creating a new Beneficiary.
/// </summary>
public class CreateBeneficiaryDto
{
    public Guid PlanId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Relationship { get; set; }
    public int? ExpectedCollegeStartYear { get; set; }
    public bool IsPrimary { get; set; }
}

/// <summary>
/// Data transfer object for updating an existing Beneficiary.
/// </summary>
public class UpdateBeneficiaryDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Relationship { get; set; }
    public int? ExpectedCollegeStartYear { get; set; }
    public bool IsPrimary { get; set; }
}
