// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MorningRoutineBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the CompletionLog entity.
/// </summary>
public class CompletionLogConfiguration : IEntityTypeConfiguration<CompletionLog>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CompletionLog> builder)
    {
        builder.ToTable("CompletionLogs");

        builder.HasKey(x => x.CompletionLogId);

        builder.Property(x => x.CompletionLogId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RoutineId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CompletionDate)
            .IsRequired();

        builder.Property(x => x.ActualStartTime);

        builder.Property(x => x.ActualEndTime);

        builder.Property(x => x.TasksCompleted)
            .IsRequired();

        builder.Property(x => x.TotalTasks)
            .IsRequired();

        builder.Property(x => x.Notes);

        builder.Property(x => x.MoodRating);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.RoutineId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CompletionDate);
        builder.HasIndex(x => new { x.UserId, x.CompletionDate });
    }
}
