// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using ProfessionalReadingList.Core;

using ProfessionalReadingList.Core.Models.UserAggregate;
using ProfessionalReadingList.Core.Models.UserAggregate.Entities;
namespace ProfessionalReadingList.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ProfessionalReadingList system.
/// </summary>
public class ProfessionalReadingListContext : DbContext, IProfessionalReadingListContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfessionalReadingListContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ProfessionalReadingListContext(DbContextOptions<ProfessionalReadingListContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Resource> Resources { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReadingProgress> ReadingProgress { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Note> Notes { get; set; } = null!;


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
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Resource>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ReadingProgress>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Note>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfessionalReadingListContext).Assembly);
    }
}
