// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core;

public class Wishlist
{
    public Guid WishlistId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Platform? Platform { get; set; }
    public Genre? Genre { get; set; }
    public int Priority { get; set; } = 3;
    public string? Notes { get; set; }
    public bool IsAcquired { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
