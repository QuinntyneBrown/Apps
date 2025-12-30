// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeImprovementProjectManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Budget entity.
/// </summary>
public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(x => x.BudgetId);

        builder.Property(x => x.BudgetId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.AllocatedAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.SpentAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ProjectId);
        builder.HasIndex(x => x.Category);
    }
}
