// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using BucketListManager.Core.Model.UserAggregate;
using BucketListManager.Core.Model.UserAggregate.Entities;
namespace BucketListManager.Core;

public interface IBucketListManagerContext
{
    DbSet<BucketListItem> BucketListItems { get; set; }
    DbSet<Milestone> Milestones { get; set; }
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
