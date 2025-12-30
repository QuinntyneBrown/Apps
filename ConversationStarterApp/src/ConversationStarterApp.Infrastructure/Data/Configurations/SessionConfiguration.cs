// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConversationStarterApp.Infrastructure.Data.Configurations;

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

        builder.Property(s => s.UserId)
            .IsRequired();

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.StartTime)
            .IsRequired();

        builder.Property(s => s.Participants)
            .HasMaxLength(500);

        builder.Property(s => s.PromptsUsed)
            .HasMaxLength(1000);

        builder.Property(s => s.Notes)
            .HasMaxLength(2000);

        builder.Property(s => s.WasSuccessful)
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .IsRequired();
    }
}
