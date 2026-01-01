// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;

using PersonalWiki.Core.Model.UserAggregate;
using PersonalWiki.Core.Model.UserAggregate.Entities;
namespace PersonalWiki.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalWiki system.
/// </summary>
public class PersonalWikiContext : DbContext, IPersonalWikiContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalWikiContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalWikiContext(DbContextOptions<PersonalWikiContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<WikiPage> Pages { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PageRevision> Revisions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WikiCategory> Categories { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PageLink> Links { get; set; } = null!;


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
            modelBuilder.Entity<WikiPage>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<PageRevision>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WikiCategory>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<PageLink>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalWikiContext).Assembly);
    }
}
