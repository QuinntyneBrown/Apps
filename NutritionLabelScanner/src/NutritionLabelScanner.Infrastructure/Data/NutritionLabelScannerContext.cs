// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the NutritionLabelScanner system.
/// </summary>
public class NutritionLabelScannerContext : DbContext, INutritionLabelScannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NutritionLabelScannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public NutritionLabelScannerContext(DbContextOptions<NutritionLabelScannerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Product> Products { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<NutritionInfo> NutritionInfos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Comparison> Comparisons { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionLabelScannerContext).Assembly);
    }
}
