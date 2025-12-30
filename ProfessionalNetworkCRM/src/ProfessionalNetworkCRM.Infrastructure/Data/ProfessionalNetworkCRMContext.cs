// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ProfessionalNetworkCRM system.
/// </summary>
public class ProfessionalNetworkCRMContext : DbContext, IProfessionalNetworkCRMContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfessionalNetworkCRMContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ProfessionalNetworkCRMContext(DbContextOptions<ProfessionalNetworkCRMContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Contact> Contacts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Interaction> Interactions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<FollowUp> FollowUps { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Contact>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Interaction>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<FollowUp>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfessionalNetworkCRMContext).Assembly);
    }
}
