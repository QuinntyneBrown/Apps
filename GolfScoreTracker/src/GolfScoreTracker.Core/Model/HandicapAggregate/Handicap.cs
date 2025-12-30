// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public class Handicap
{
    public Guid HandicapId { get; set; }
    public Guid UserId { get; set; }
    public decimal HandicapIndex { get; set; }
    public DateTime CalculatedDate { get; set; } = DateTime.UtcNow;
    public int RoundsUsed { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsCurrentlyValid()
    {
        return CalculatedDate >= DateTime.UtcNow.AddMonths(-1);
    }
}
