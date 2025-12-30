// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core;

public class PackingList
{
    public Guid PackingListId { get; set; }
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public bool IsPacked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
