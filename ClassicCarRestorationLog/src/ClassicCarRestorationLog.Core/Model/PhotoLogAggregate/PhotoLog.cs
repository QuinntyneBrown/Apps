// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core;

public class PhotoLog
{
    public Guid PhotoLogId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public DateTime PhotoDate { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public ProjectPhase? Phase { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
