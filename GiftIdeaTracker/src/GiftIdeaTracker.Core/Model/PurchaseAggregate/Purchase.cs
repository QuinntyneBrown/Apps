// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core;

public class Purchase
{
    public Guid PurchaseId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid GiftIdeaId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal ActualPrice { get; set; }
    public string? Store { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
