// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RoadsideAssistanceInfoHub.Core;
using Microsoft.EntityFrameworkCore;

namespace RoadsideAssistanceInfoHub.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RoadsideAssistanceInfoHub system.
/// </summary>
public class RoadsideAssistanceInfoHubContext : DbContext, IRoadsideAssistanceInfoHubContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoadsideAssistanceInfoHubContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RoadsideAssistanceInfoHubContext(DbContextOptions<RoadsideAssistanceInfoHubContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<InsuranceInfo> InsuranceInfos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<EmergencyContact> EmergencyContacts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Policy> Policies { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoadsideAssistanceInfoHubContext).Assembly);
    }
}
