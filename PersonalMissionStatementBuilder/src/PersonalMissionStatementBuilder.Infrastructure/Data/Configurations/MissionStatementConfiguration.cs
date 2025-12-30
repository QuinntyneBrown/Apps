// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalMissionStatementBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalMissionStatementBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MissionStatement entity.
/// </summary>
public class MissionStatementConfiguration : IEntityTypeConfiguration<MissionStatement>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MissionStatement> builder)
    {
        builder.ToTable("MissionStatements");

        builder.HasKey(x => x.MissionStatementId);

        builder.Property(x => x.MissionStatementId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Text)
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(x => x.Version)
            .IsRequired();

        builder.Property(x => x.IsCurrentVersion)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.StatementDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Values)
            .WithOne(x => x.MissionStatement)
            .HasForeignKey(x => x.MissionStatementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Goals)
            .WithOne(x => x.MissionStatement)
            .HasForeignKey(x => x.MissionStatementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.IsCurrentVersion });
    }
}
