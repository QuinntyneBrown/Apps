// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using DocumentVaultOrganizer.Core.Model.UserAggregate;
using DocumentVaultOrganizer.Core.Model.UserAggregate.Entities;
namespace DocumentVaultOrganizer.Core;

public interface IDocumentVaultOrganizerContext
{
    DbSet<Document> Documents { get; set; }
    DbSet<DocumentCategory> DocumentCategories { get; set; }
    DbSet<ExpirationAlert> ExpirationAlerts { get; set; }
    
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
