// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Represents a roadside assistance policy or service plan.
/// </summary>
public class Policy
{
    /// <summary>
    /// Gets or sets the unique identifier for the policy.
    /// </summary>
    public Guid PolicyId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the policy provider name.
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the policy number.
    /// </summary>
    public string PolicyNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the policy start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the policy end date.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the emergency phone number for this policy.
    /// </summary>
    public string EmergencyPhone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum towing distance in miles.
    /// </summary>
    public int? MaxTowingDistance { get; set; }

    /// <summary>
    /// Gets or sets the number of service calls allowed per year.
    /// </summary>
    public int? ServiceCallsPerYear { get; set; }

    /// <summary>
    /// Gets or sets the list of covered services.
    /// </summary>
    public List<string> CoveredServices { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the annual premium amount.
    /// </summary>
    public decimal? AnnualPremium { get; set; }

    /// <summary>
    /// Gets or sets whether battery service is covered.
    /// </summary>
    public bool CoversBatteryService { get; set; }

    /// <summary>
    /// Gets or sets whether flat tire service is covered.
    /// </summary>
    public bool CoversFlatTire { get; set; }

    /// <summary>
    /// Gets or sets whether fuel delivery is covered.
    /// </summary>
    public bool CoversFuelDelivery { get; set; }

    /// <summary>
    /// Gets or sets whether lockout service is covered.
    /// </summary>
    public bool CoversLockout { get; set; }

    /// <summary>
    /// Gets or sets additional policy notes.
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
        return currentDate >= StartDate && currentDate <= EndDate;
    }

    /// <summary>
    /// Determines if the policy is expiring soon.
    /// </summary>
    /// <param name="currentDate">The current date.</param>
    /// <param name="daysThreshold">The number of days to consider as "soon".</param>
    /// <returns>True if expiring within the threshold, otherwise false.</returns>
    public bool IsExpiringSoon(DateTime currentDate, int daysThreshold = 30)
    {
        var daysUntilExpiration = (EndDate - currentDate).Days;
        return daysUntilExpiration >= 0 && daysUntilExpiration <= daysThreshold;
    }

    /// <summary>
    /// Adds a covered service to the policy.
    /// </summary>
    /// <param name="services">The services to add.</param>
    public void AddCoveredServices(IEnumerable<string> services)
    {
        CoveredServices.AddRange(services);
    }

    /// <summary>
    /// Renews the policy for another term.
    /// </summary>
    /// <param name="newEndDate">The new end date.</param>
    public void RenewPolicy(DateTime newEndDate)
    {
        StartDate = EndDate;
        EndDate = newEndDate;
    }
}
