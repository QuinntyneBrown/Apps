// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public class GearChecklist
{
    public Guid GearChecklistId { get; set; }
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public bool IsPacked { get; set; }
    public int Quantity { get; set; } = 1;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
