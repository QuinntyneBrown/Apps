// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using Microsoft.EntityFrameworkCore;

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

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
