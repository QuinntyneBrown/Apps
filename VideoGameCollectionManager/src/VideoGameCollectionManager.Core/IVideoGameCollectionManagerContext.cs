// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using VideoGameCollectionManager.Core.Model.UserAggregate;
using VideoGameCollectionManager.Core.Model.UserAggregate.Entities;
namespace VideoGameCollectionManager.Core;

public interface IVideoGameCollectionManagerContext
{
    DbSet<Game> Games { get; set; }
    DbSet<PlaySession> PlaySessions { get; set; }
    DbSet<Wishlist> Wishlists { get; set; }
    
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
