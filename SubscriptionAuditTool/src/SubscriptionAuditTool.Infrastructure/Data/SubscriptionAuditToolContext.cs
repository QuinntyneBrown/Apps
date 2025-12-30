// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SubscriptionAuditTool.Core;
using Microsoft.EntityFrameworkCore;

namespace SubscriptionAuditTool.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SubscriptionAuditTool system.
/// </summary>
public class SubscriptionAuditToolContext : DbContext, ISubscriptionAuditToolContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionAuditToolContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SubscriptionAuditToolContext(DbContextOptions<SubscriptionAuditToolContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Category> Categories { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionAuditToolContext).Assembly);
    }
}
