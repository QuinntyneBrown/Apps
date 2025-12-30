// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreAssignmentTracker.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="FamilyMember"/> entity.
/// </summary>
public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    /// <summary>
    /// Configures the entity of type <see cref="FamilyMember"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.HasKey(f => f.FamilyMemberId);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Avatar)
            .HasMaxLength(500);

        builder.Property(f => f.TotalPoints)
            .IsRequired();

        builder.Property(f => f.AvailablePoints)
            .IsRequired();

        builder.Property(f => f.IsActive)
            .IsRequired();

        builder.Property(f => f.CreatedAt)
            .IsRequired();

        builder.HasMany(f => f.Assignments)
            .WithOne(a => a.FamilyMember)
            .HasForeignKey(a => a.FamilyMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.RedeemedRewards)
            .WithOne(r => r.RedeemedByFamilyMember)
            .HasForeignKey(r => r.RedeemedByFamilyMemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
