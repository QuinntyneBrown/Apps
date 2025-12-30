// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeSavingsPlanner.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Plan"/> entity.
/// </summary>
public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    /// <summary>
    /// Configures the entity of type <see cref="Plan"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(p => p.PlanId);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.State)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.AccountNumber)
            .HasMaxLength(50);

        builder.Property(p => p.CurrentBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.OpenedDate)
            .IsRequired();

        builder.Property(p => p.Administrator)
            .HasMaxLength(200);

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.Property(p => p.Notes)
            .HasMaxLength(1000);
    }
}
