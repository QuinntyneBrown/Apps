// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core;

public class Contractor
{
    public Guid ContractorId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Trade { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public int? Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
