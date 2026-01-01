using System.Security.Claims;
using ContactManagementApp.Core;
using Microsoft.AspNetCore.Http;

namespace ContactManagementApp.Infrastructure;

/// <summary>
/// Provides tenant context by extracting tenant information from the current HTTP context.
/// </summary>
public class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid TenantId
    {
        get
        {
            var tenantClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id");
            if (tenantClaim != null && Guid.TryParse(tenantClaim.Value, out var tenantId))
            {
                return tenantId;
            }

            var tenantHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].FirstOrDefault();
            if (!string.IsNullOrEmpty(tenantHeader) && Guid.TryParse(tenantHeader, out var headerTenantId))
            {
                return headerTenantId;
            }

            return Guid.Empty;
        }
    }

    public bool HasTenant => TenantId != Guid.Empty;
}
