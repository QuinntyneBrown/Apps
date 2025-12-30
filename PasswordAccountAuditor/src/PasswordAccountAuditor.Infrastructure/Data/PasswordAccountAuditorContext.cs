// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;
using Microsoft.EntityFrameworkCore;

namespace PasswordAccountAuditor.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PasswordAccountAuditor system.
/// </summary>
public class PasswordAccountAuditorContext : DbContext, IPasswordAccountAuditorContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordAccountAuditorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PasswordAccountAuditorContext(DbContextOptions<PasswordAccountAuditorContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Account> Accounts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SecurityAudit> SecurityAudits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<BreachAlert> BreachAlerts { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Account>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SecurityAudit>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<BreachAlert>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PasswordAccountAuditorContext).Assembly);
    }
}
