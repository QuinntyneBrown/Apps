// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;

using PetCareManager.Core.Model.UserAggregate;
using PetCareManager.Core.Model.UserAggregate.Entities;
namespace PetCareManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PetCareManager system.
/// </summary>
public class PetCareManagerContext : DbContext, IPetCareManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PetCareManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PetCareManagerContext(DbContextOptions<PetCareManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Pet> Pets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<VetAppointment> VetAppointments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Medication> Medications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Vaccination> Vaccinations { get; set; } = null!;


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Apply tenant filter to User
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);

        // Apply tenant filter to Role
        modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);

        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Pet>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<VetAppointment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Medication>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Vaccination>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetCareManagerContext).Assembly);
    }
}
