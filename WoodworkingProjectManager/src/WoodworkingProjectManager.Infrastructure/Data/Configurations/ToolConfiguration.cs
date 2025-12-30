// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WoodworkingProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WoodworkingProjectManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Tool entity.
/// </summary>
public class ToolConfiguration : IEntityTypeConfiguration<Tool>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Tool> builder)
    {
        builder.ToTable("Tools");

        builder.HasKey(x => x.ToolId);

        builder.Property(x => x.ToolId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Brand)
            .HasMaxLength(100);

        builder.Property(x => x.Model)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.PurchaseDate);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Brand);
    }
}
