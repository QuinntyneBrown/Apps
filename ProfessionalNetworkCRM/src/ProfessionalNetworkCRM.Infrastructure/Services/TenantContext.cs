// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ProfessionalNetworkCRM.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// Provides tenant context by extracting tenant information from the current HTTP context.
/// </summary>
public class TenantContext : ITenantContext
{
    private static readonly Guid DefaultDevelopmentTenantId = Constants.DefaultTenantId;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _environment;

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantContext"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public TenantContext(IHttpContextAccessor httpContextAccessor, IHostEnvironment environment)
    {
        _httpContextAccessor = httpContextAccessor;
        _environment = environment;
    }

    /// <inheritdoc/>
    public Guid TenantId
    {
        get
        {
            var tenantClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id");
            if (tenantClaim != null && Guid.TryParse(tenantClaim.Value, out var tenantId))
            {
                return tenantId;
            }

            // Fallback to header-based tenant resolution
            var tenantHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].FirstOrDefault();
            if (!string.IsNullOrEmpty(tenantHeader) && Guid.TryParse(tenantHeader, out var headerTenantId))
            {
                return headerTenantId;
            }

            if (_environment.IsDevelopment())
            {
                return DefaultDevelopmentTenantId;
            }

            return Guid.Empty;
        }
    }

    /// <inheritdoc/>
    public bool HasTenant => TenantId != Guid.Empty;
}
