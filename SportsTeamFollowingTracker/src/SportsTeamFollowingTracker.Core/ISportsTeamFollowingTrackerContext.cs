// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace SportsTeamFollowingTracker.Core;

public interface ISportsTeamFollowingTrackerContext
{
    DbSet<Team> Teams { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<Season> Seasons { get; set; }
    DbSet<Statistic> Statistics { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
