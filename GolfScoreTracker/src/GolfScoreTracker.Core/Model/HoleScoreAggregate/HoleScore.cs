// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public class HoleScore
{
    public Guid HoleScoreId { get; set; }
    public Guid RoundId { get; set; }
    public int HoleNumber { get; set; }
    public int Par { get; set; }
    public int Score { get; set; }
    public int? Putts { get; set; }
    public bool FairwayHit { get; set; }
    public bool GreenInRegulation { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Round? Round { get; set; }
    
    public int GetScoreToPar()
    {
        return Score - Par;
    }
}
