// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using MusicCollectionOrganizer.Core.Model.UserAggregate;
using MusicCollectionOrganizer.Core.Model.UserAggregate.Entities;
namespace MusicCollectionOrganizer.Core;

public interface IMusicCollectionOrganizerContext
{
    DbSet<Album> Albums { get; set; }
    DbSet<Artist> Artists { get; set; }
    DbSet<ListeningLog> ListeningLogs { get; set; }
    
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
