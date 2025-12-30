// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public class Season
{
    public Guid SeasonId { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
