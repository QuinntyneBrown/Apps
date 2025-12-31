// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using ClassicCarRestorationLog.Core.Model.UserAggregate;
using ClassicCarRestorationLog.Core.Model.UserAggregate.Entities;
namespace ClassicCarRestorationLog.Core;

public interface IClassicCarRestorationLogContext
{
    DbSet<Project> Projects { get; set; }
    DbSet<Part> Parts { get; set; }
    DbSet<WorkLog> WorkLogs { get; set; }
    DbSet<PhotoLog> PhotoLogs { get; set; }
    
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
