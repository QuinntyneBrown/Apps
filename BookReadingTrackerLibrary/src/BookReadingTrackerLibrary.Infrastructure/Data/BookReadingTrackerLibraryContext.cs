// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;

using BookReadingTrackerLibrary.Core.Models.UserAggregate;
using BookReadingTrackerLibrary.Core.Models.UserAggregate.Entities;
namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BookReadingTrackerLibrary system.
/// </summary>
public class BookReadingTrackerLibraryContext : DbContext, IBookReadingTrackerLibraryContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookReadingTrackerLibraryContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BookReadingTrackerLibraryContext(DbContextOptions<BookReadingTrackerLibraryContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Book> Books { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReadingLog> ReadingLogs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Wishlist> Wishlists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Review> Reviews { get; set; } = null!;


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
            modelBuilder.Entity<Book>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ReadingLog>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Wishlist>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Review>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookReadingTrackerLibraryContext).Assembly);
    }
}
