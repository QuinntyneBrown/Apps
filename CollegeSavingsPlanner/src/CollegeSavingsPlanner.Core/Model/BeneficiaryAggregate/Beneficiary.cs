// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Represents a beneficiary of a 529 plan.
/// </summary>
public class Beneficiary
{
    /// <summary>
    /// Gets or sets the unique identifier for the beneficiary.
    /// </summary>
    public Guid BeneficiaryId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the plan.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Gets or sets the beneficiary's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the beneficiary's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the beneficiary's date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the relationship to the account owner.
    /// </summary>
    public string? Relationship { get; set; }

    /// <summary>
    /// Gets or sets the expected college start year.
    /// </summary>
    public int? ExpectedCollegeStartYear { get; set; }

    /// <summary>
    /// Gets or sets whether this is the primary beneficiary.
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the plan.
    /// </summary>
    public Plan? Plan { get; set; }

    /// <summary>
    /// Calculates the age of the beneficiary.
    /// </summary>
    /// <returns>The current age.</returns>
    public int CalculateAge()
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    /// <summary>
    /// Calculates years until college.
    /// </summary>
    /// <returns>Years until expected college start, or null if not set.</returns>
    public int? CalculateYearsUntilCollege()
    {
        if (!ExpectedCollegeStartYear.HasValue)
        {
            return null;
        }

        return ExpectedCollegeStartYear.Value - DateTime.Now.Year;
    }
}
