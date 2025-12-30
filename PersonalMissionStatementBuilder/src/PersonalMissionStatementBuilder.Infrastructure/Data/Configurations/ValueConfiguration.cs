// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalMissionStatementBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalMissionStatementBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Value entity.
/// </summary>
public class ValueConfiguration : IEntityTypeConfiguration<Value>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Value> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(x => x.ValueId);

        builder.Property(x => x.ValueId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MissionStatementId);
        builder.HasIndex(x => new { x.UserId, x.Priority });
    }
}
