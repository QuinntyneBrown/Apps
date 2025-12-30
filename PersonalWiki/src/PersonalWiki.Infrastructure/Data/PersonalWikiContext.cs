// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalWiki.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalWiki system.
/// </summary>
public class PersonalWikiContext : DbContext, IPersonalWikiContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalWikiContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalWikiContext(DbContextOptions<PersonalWikiContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<WikiPage> Pages { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PageRevision> Revisions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WikiCategory> Categories { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PageLink> Links { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalWikiContext).Assembly);
    }
}
