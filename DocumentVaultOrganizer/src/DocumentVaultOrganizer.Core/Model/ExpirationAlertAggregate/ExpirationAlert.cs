// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core;

public class ExpirationAlert
{
    public Guid ExpirationAlertId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid DocumentId { get; set; }
    public DateTime AlertDate { get; set; }
    public bool IsAcknowledged { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
