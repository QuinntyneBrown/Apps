// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the FollowUp entity.
/// </summary>
public class FollowUpConfiguration : IEntityTypeConfiguration<FollowUp>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<FollowUp> builder)
    {
        builder.ToTable("FollowUps");

        builder.HasKey(x => x.FollowUpId);

        builder.Property(x => x.FollowUpId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ContactId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.Priority)
            .HasMaxLength(50);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ContactId);
        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => x.IsCompleted);

        builder.HasOne(x => x.Contact)
            .WithMany(x => x.FollowUps)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
