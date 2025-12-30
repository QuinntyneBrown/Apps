// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreAssignmentTracker.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Chore"/> entity.
/// </summary>
public class ChoreConfiguration : IEntityTypeConfiguration<Chore>
{
    /// <summary>
    /// Configures the entity of type <see cref="Chore"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Chore> builder)
    {
        builder.HasKey(c => c.ChoreId);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(1000);

        builder.Property(c => c.Category)
            .HasMaxLength(100);

        builder.Property(c => c.Frequency)
            .IsRequired();

        builder.Property(c => c.Points)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasMany(c => c.Assignments)
            .WithOne(a => a.Chore)
            .HasForeignKey(a => a.ChoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
