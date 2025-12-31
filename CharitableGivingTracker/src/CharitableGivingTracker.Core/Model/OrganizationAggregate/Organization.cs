// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core;

public class Organization
{
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? EIN { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }
    public bool Is501c3 { get; set; } = true;
    public string? Notes { get; set; }
    public List<Donation> Donations { get; set; } = new List<Donation>();
    
    public decimal CalculateTotalDonations()
    {
        return Donations.Sum(d => d.Amount);
    }
}
