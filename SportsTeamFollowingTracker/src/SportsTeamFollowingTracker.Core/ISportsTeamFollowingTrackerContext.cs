// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using SportsTeamFollowingTracker.Core.Models.UserAggregate;
using SportsTeamFollowingTracker.Core.Models.UserAggregate.Entities;
namespace SportsTeamFollowingTracker.Core;

public interface ISportsTeamFollowingTrackerContext
{
    DbSet<Team> Teams { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<Season> Seasons { get; set; }
    DbSet<Statistic> Statistics { get; set; }
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
