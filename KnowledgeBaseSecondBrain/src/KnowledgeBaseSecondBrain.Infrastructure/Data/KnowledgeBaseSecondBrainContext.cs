// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KnowledgeBaseSecondBrain.Core;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseSecondBrain.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the KnowledgeBaseSecondBrain system.
/// </summary>
public class KnowledgeBaseSecondBrainContext : DbContext, IKnowledgeBaseSecondBrainContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="KnowledgeBaseSecondBrainContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public KnowledgeBaseSecondBrainContext(DbContextOptions<KnowledgeBaseSecondBrainContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Note> Notes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Tag> Tags { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<NoteLink> Links { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SearchQuery> SearchQueries { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Note>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Tag>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<NoteLink>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SearchQuery>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KnowledgeBaseSecondBrainContext).Assembly);
    }
}
