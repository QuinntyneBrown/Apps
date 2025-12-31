// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core;

public class DocumentCategory
{
    public Guid DocumentCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
