// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core;

public class Session
{
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public SessionType SessionType { get; set; }
    public DateTime SessionDate { get; set; } = DateTime.UtcNow;
    public string? Location { get; set; }
    public string? Client { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
