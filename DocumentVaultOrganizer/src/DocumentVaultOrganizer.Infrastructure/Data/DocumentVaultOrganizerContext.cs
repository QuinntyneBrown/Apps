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
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentVaultOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DocumentVaultOrganizerContext(DbContextOptions<DocumentVaultOrganizerContext> options)
        : base(options)
    {
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentVaultOrganizerContext).Assembly);
    }
}
