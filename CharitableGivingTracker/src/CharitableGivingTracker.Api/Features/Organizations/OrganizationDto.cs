// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Api.Features.Organizations;

public class OrganizationDto
{
    public Guid OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? EIN { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }
    public bool Is501c3 { get; set; }
    public string? Notes { get; set; }
    public decimal TotalDonations { get; set; }
}
