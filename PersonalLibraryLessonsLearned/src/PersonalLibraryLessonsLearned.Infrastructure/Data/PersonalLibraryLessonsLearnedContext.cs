// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLibraryLessonsLearned.Core;
using Microsoft.EntityFrameworkCore;

using PersonalLibraryLessonsLearned.Core.Model.UserAggregate;
using PersonalLibraryLessonsLearned.Core.Model.UserAggregate.Entities;
namespace PersonalLibraryLessonsLearned.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalLibraryLessonsLearned system.
/// </summary>
public class PersonalLibraryLessonsLearnedContext : DbContext, IPersonalLibraryLessonsLearnedContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalLibraryLessonsLearnedContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalLibraryLessonsLearnedContext(DbContextOptions<PersonalLibraryLessonsLearnedContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Lesson> Lessons { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Source> Sources { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LessonReminder> Reminders { get; set; } = null!;


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
            modelBuilder.Entity<Lesson>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Source>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<LessonReminder>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalLibraryLessonsLearnedContext).Assembly);
    }
}
