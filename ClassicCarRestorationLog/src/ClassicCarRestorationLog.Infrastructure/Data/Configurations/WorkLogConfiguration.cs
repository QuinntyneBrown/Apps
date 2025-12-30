// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassicCarRestorationLog.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="WorkLog"/> entity.
/// </summary>
public class WorkLogConfiguration : IEntityTypeConfiguration<WorkLog>
{
    /// <summary>
    /// Configures the entity of type <see cref="WorkLog"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<WorkLog> builder)
    {
        builder.HasKey(w => w.WorkLogId);

        builder.Property(w => w.ProjectId)
            .IsRequired();

        builder.Property(w => w.WorkDate)
            .IsRequired();

        builder.Property(w => w.HoursWorked)
            .IsRequired();

        builder.Property(w => w.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(w => w.WorkPerformed)
            .HasMaxLength(2000);

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        builder.HasOne(w => w.Project)
            .WithMany(p => p.WorkLogs)
            .HasForeignKey(w => w.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
