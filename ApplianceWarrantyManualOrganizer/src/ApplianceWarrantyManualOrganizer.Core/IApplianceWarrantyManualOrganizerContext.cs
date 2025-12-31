// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using ApplianceWarrantyManualOrganizer.Core.Model.UserAggregate;
using ApplianceWarrantyManualOrganizer.Core.Model.UserAggregate.Entities;
namespace ApplianceWarrantyManualOrganizer.Core;

public interface IApplianceWarrantyManualOrganizerContext
{
    DbSet<Appliance> Appliances { get; set; }
    DbSet<Warranty> Warranties { get; set; }
    DbSet<Manual> Manuals { get; set; }
    DbSet<ServiceRecord> ServiceRecords { get; set; }
    
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
