// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public class Statistic
{
    public Guid StatisticId { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public string StatName { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
