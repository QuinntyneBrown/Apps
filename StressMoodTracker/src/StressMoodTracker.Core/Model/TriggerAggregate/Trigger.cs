// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core;

public class Trigger
{
    public Guid TriggerId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string TriggerType { get; set; } = string.Empty;
    public int ImpactLevel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsHighImpact()
    {
        return ImpactLevel >= 4;
    }
}
