// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Provides tenant context for multi-tenant isolation.
/// </summary>
public interface ITenantContext
{
    /// <summary>
    /// Gets the current tenant ID.
    /// </summary>
    Guid TenantId { get; }

    /// <summary>
    /// Gets a value indicating whether a tenant context is available.
    /// </summary>
    bool HasTenant { get; }
}
