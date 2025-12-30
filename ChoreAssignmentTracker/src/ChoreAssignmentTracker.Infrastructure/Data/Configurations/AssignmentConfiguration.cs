// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreAssignmentTracker.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Assignment"/> entity.
/// </summary>
public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    /// <summary>
    /// Configures the entity of type <see cref="Assignment"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(a => a.AssignmentId);

        builder.Property(a => a.ChoreId)
            .IsRequired();

        builder.Property(a => a.FamilyMemberId)
            .IsRequired();

        builder.Property(a => a.AssignedDate)
            .IsRequired();

        builder.Property(a => a.DueDate)
            .IsRequired();

        builder.Property(a => a.IsCompleted)
            .IsRequired();

        builder.Property(a => a.IsVerified)
            .IsRequired();

        builder.Property(a => a.Notes)
            .HasMaxLength(500);

        builder.Property(a => a.PointsEarned)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.HasOne(a => a.Chore)
            .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.ChoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.FamilyMember)
            .WithMany(f => f.Assignments)
            .HasForeignKey(a => a.FamilyMemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
