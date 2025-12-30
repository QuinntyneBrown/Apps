// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ProfessionalNetworkCRM system.
/// </summary>
public class ProfessionalNetworkCRMContext : DbContext, IProfessionalNetworkCRMContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfessionalNetworkCRMContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ProfessionalNetworkCRMContext(DbContextOptions<ProfessionalNetworkCRMContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Contact> Contacts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Interaction> Interactions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<FollowUp> FollowUps { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfessionalNetworkCRMContext).Assembly);
    }
}
