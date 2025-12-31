// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using Microsoft.EntityFrameworkCore;

using DocumentVaultOrganizer.Core.Model.UserAggregate;
using DocumentVaultOrganizer.Core.Model.UserAggregate.Entities;
namespace DocumentVaultOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DocumentVaultOrganizer system.
/// </summary>
public class DocumentVaultOrganizerContext : DbContext, IDocumentVaultOrganizerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentVaultOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DocumentVaultOrganizerContext(DbContextOptions<DocumentVaultOrganizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Document> Documents { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<DocumentCategory> DocumentCategories { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ExpirationAlert> ExpirationAlerts { get; set; } = null!;


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
            modelBuilder.Entity<Document>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<DocumentCategory>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ExpirationAlert>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentVaultOrganizerContext).Assembly);
    }
}
