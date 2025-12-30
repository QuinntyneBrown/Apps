// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core;

public class Warranty
{
    public Guid WarrantyId { get; set; }
    public Guid ApplianceId { get; set; }
    public Appliance? Appliance { get; set; }
    public string? Provider { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? CoverageDetails { get; set; }
    public string? DocumentUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
