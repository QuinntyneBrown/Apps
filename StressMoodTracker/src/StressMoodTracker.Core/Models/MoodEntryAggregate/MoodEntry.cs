// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core;

public class MoodEntry
{
    public Guid MoodEntryId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public MoodLevel MoodLevel { get; set; }
    public StressLevel StressLevel { get; set; }
    public DateTime EntryTime { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    public string? Activities { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsPositiveMood()
    {
        return MoodLevel == MoodLevel.Good || MoodLevel == MoodLevel.Excellent;
    }
}
