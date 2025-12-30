// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceEventManager.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Session"/> entity.
/// </summary>
public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    /// <summary>
    /// Configures the entity of type <see cref="Session"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.SessionId);

        builder.Property(s => s.EventId)
            .IsRequired();

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(s => s.Speaker)
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .HasMaxLength(2000);

        builder.Property(s => s.StartTime)
            .IsRequired();

        builder.Property(s => s.Room)
            .HasMaxLength(100);

        builder.Property(s => s.PlansToAttend)
            .IsRequired();

        builder.Property(s => s.DidAttend)
            .IsRequired();

        builder.Property(s => s.Notes)
            .HasMaxLength(1000);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.HasOne(s => s.Event)
            .WithMany(e => e.Sessions)
            .HasForeignKey(s => s.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
