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
    /// <summary>
    /// Initializes a new instance of the <see cref="KnowledgeBaseSecondBrainContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public KnowledgeBaseSecondBrainContext(DbContextOptions<KnowledgeBaseSecondBrainContext> options)
        : base(options)
    {
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KnowledgeBaseSecondBrainContext).Assembly);
    }
}
