// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework database context for the CollegeSavingsPlanner system.
/// </summary>
public class CollegeSavingsPlannerContext : DbContext, ICollegeSavingsPlannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CollegeSavingsPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public CollegeSavingsPlannerContext(DbContextOptions<CollegeSavingsPlannerContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet of 529 plans.
    /// </summary>
    public DbSet<Plan> Plans { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of contributions.
    /// </summary>
    public DbSet<Contribution> Contributions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of beneficiaries.
    /// </summary>
    public DbSet<Beneficiary> Beneficiaries { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of projections.
    /// </summary>
    public DbSet<Projection> Projections { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollegeSavingsPlannerContext).Assembly);
    }
}
