// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassicCarRestorationLog.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Part"/> entity.
/// </summary>
public class PartConfiguration : IEntityTypeConfiguration<Part>
{
    /// <summary>
    /// Configures the entity of type <see cref="Part"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.HasKey(p => p.PartId);

        builder.Property(p => p.ProjectId)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.PartNumber)
            .HasMaxLength(100);

        builder.Property(p => p.Supplier)
            .HasMaxLength(200);

        builder.Property(p => p.Cost)
            .HasPrecision(18, 2);

        builder.Property(p => p.IsInstalled)
            .IsRequired();

        builder.Property(p => p.Notes)
            .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasOne(p => p.Project)
            .WithMany(pr => pr.Parts)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
