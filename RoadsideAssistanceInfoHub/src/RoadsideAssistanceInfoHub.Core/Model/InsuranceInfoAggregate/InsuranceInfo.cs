// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Represents insurance information for a vehicle.
/// </summary>
public class InsuranceInfo
{
    /// <summary>
    /// Gets or sets the unique identifier for the insurance information.
    /// </summary>
    public Guid InsuranceInfoId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the insurance company name.
    /// </summary>
    public string InsuranceCompany { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the policy number.
    /// </summary>
    public string PolicyNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the policy holder name.
    /// </summary>
    public string PolicyHolder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the policy start date.
    /// </summary>
    public DateTime PolicyStartDate { get; set; }

    /// <summary>
    /// Gets or sets the policy end date.
    /// </summary>
    public DateTime PolicyEndDate { get; set; }

    /// <summary>
    /// Gets or sets the insurance agent name.
    /// </summary>
    public string? AgentName { get; set; }

    /// <summary>
    /// Gets or sets the insurance agent phone number.
    /// </summary>
    public string? AgentPhone { get; set; }

    /// <summary>
    /// Gets or sets the insurance company phone number.
    /// </summary>
    public string? CompanyPhone { get; set; }

    /// <summary>
    /// Gets or sets the claims phone number.
    /// </summary>
    public string? ClaimsPhone { get; set; }

    /// <summary>
    /// Gets or sets the coverage type.
    /// </summary>
    public string? CoverageType { get; set; }

    /// <summary>
    /// Gets or sets the deductible amount.
    /// </summary>
    public decimal? Deductible { get; set; }

    /// <summary>
    /// Gets or sets the premium amount.
    /// </summary>
    public decimal? Premium { get; set; }

    /// <summary>
    /// Gets or sets whether roadside assistance is included.
    /// </summary>
    public bool IncludesRoadsideAssistance { get; set; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Determines if the policy is currently active.
    /// </summary>
    /// <param name="currentDate">The current date.</param>
    /// <returns>True if the policy is active, otherwise false.</returns>
    public bool IsActive(DateTime currentDate)
    {
        return currentDate >= PolicyStartDate && currentDate <= PolicyEndDate;
    }

    /// <summary>
    /// Determines if the policy is expiring soon.
    /// </summary>
    /// <param name="currentDate">The current date.</param>
    /// <param name="daysThreshold">The number of days to consider as "soon".</param>
    /// <returns>True if expiring within the threshold, otherwise false.</returns>
    public bool IsExpiringSoon(DateTime currentDate, int daysThreshold = 30)
    {
        var daysUntilExpiration = (PolicyEndDate - currentDate).Days;
        return daysUntilExpiration >= 0 && daysUntilExpiration <= daysThreshold;
    }

    /// <summary>
    /// Renews the policy for another term.
    /// </summary>
    /// <param name="newEndDate">The new end date.</param>
    public void RenewPolicy(DateTime newEndDate)
    {
        PolicyStartDate = PolicyEndDate;
        PolicyEndDate = newEndDate;
    }
}
