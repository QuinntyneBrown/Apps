// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Interview entity.
/// </summary>
public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder.ToTable("Interviews");

        builder.HasKey(x => x.InterviewId);

        builder.Property(x => x.InterviewId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ApplicationId)
            .IsRequired();

        builder.Property(x => x.InterviewType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ScheduledDateTime)
            .IsRequired();

        builder.Property(x => x.DurationMinutes);

        builder.Property(x => x.Interviewers)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.PreparationNotes)
            .HasMaxLength(2000);

        builder.Property(x => x.Feedback)
            .HasMaxLength(2000);

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ApplicationId);
        builder.HasIndex(x => x.ScheduledDateTime);
        builder.HasIndex(x => x.IsCompleted);
    }
}
