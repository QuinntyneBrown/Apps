// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FamilyTreeBuilder.Core.Model.UserAggregate;
using FamilyTreeBuilder.Core.Model.UserAggregate.Entities;
namespace FamilyTreeBuilder.Core;

public interface IFamilyTreeBuilderContext
{
    DbSet<Person> Persons { get; set; }
    DbSet<Relationship> Relationships { get; set; }
    DbSet<Story> Stories { get; set; }
    DbSet<FamilyPhoto> FamilyPhotos { get; set; }
    
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
