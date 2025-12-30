// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GolfScoreTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Course entity.
/// </summary>
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(x => x.CourseId);

        builder.Property(x => x.CourseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.NumberOfHoles)
            .IsRequired();

        builder.Property(x => x.TotalPar)
            .IsRequired();

        builder.Property(x => x.CourseRating)
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.SlopeRating);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Location);
    }
}
