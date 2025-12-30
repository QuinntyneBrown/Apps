// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreAssignmentTracker.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Reward"/> entity.
/// </summary>
public class RewardConfiguration : IEntityTypeConfiguration<Reward>
{
    /// <summary>
    /// Configures the entity of type <see cref="Reward"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Reward> builder)
    {
        builder.HasKey(r => r.RewardId);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.Property(r => r.PointCost)
            .IsRequired();

        builder.Property(r => r.Category)
            .HasMaxLength(100);

        builder.Property(r => r.IsAvailable)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.HasOne(r => r.RedeemedByFamilyMember)
            .WithMany(f => f.RedeemedRewards)
            .HasForeignKey(r => r.RedeemedByFamilyMemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
