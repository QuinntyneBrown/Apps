// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core;

public class Manual
{
    public Guid ManualId { get; set; }
    public Guid ApplianceId { get; set; }
    public Appliance? Appliance { get; set; }
    public string? Title { get; set; }
    public string? FileUrl { get; set; }
    public string? FileType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
