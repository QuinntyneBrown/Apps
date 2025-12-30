// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using Microsoft.EntityFrameworkCore;

namespace ConversationStarterApp.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework database context for the ConversationStarterApp system.
/// </summary>
public class ConversationStarterAppContext : DbContext, IConversationStarterAppContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConversationStarterAppContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ConversationStarterAppContext(DbContextOptions<ConversationStarterAppContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet of prompts.
    /// </summary>
    public DbSet<Prompt> Prompts { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of favorites.
    /// </summary>
    public DbSet<Favorite> Favorites { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of sessions.
    /// </summary>
    public DbSet<Session> Sessions { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConversationStarterAppContext).Assembly);
    }
}
