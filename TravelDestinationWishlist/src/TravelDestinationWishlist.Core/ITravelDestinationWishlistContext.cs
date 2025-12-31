// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using TravelDestinationWishlist.Core.Model.UserAggregate;
using TravelDestinationWishlist.Core.Model.UserAggregate.Entities;
namespace TravelDestinationWishlist.Core;

public interface ITravelDestinationWishlistContext
{
    DbSet<Destination> Destinations { get; set; }
    DbSet<Trip> Trips { get; set; }
    DbSet<Memory> Memories { get; set; }
    
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
