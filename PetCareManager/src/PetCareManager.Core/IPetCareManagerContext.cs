// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using PetCareManager.Core.Model.UserAggregate;
using PetCareManager.Core.Model.UserAggregate.Entities;
namespace PetCareManager.Core;

public interface IPetCareManagerContext
{
    DbSet<Pet> Pets { get; set; }
    DbSet<VetAppointment> VetAppointments { get; set; }
    DbSet<Medication> Medications { get; set; }
    DbSet<Vaccination> Vaccinations { get; set; }
    
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
