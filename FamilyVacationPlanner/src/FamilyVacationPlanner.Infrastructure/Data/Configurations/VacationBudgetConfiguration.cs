// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the VacationBudget entity.
/// </summary>
public class VacationBudgetConfiguration : IEntityTypeConfiguration<VacationBudget>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<VacationBudget> builder)
    {
        builder.ToTable("VacationBudgets");

        builder.HasKey(x => x.VacationBudgetId);

        builder.Property(x => x.VacationBudgetId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TripId)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.AllocatedAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.SpentAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.TripId);
        builder.HasIndex(x => x.Category);
    }
}
