// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FamilyTreeBuilder system.
/// </summary>
public class FamilyTreeBuilderContext : DbContext, IFamilyTreeBuilderContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyTreeBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FamilyTreeBuilderContext(DbContextOptions<FamilyTreeBuilderContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Person> Persons { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Relationship> Relationships { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Story> Stories { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<FamilyPhoto> FamilyPhotos { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyTreeBuilderContext).Assembly);
    }
}
