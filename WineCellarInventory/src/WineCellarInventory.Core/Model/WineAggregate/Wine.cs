// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Core;

public class Wine
{
    public Guid WineId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public WineType WineType { get; set; }
    public Region Region { get; set; }
    public int? Vintage { get; set; }
    public string? Producer { get; set; }
    public decimal? PurchasePrice { get; set; }
    public int BottleCount { get; set; } = 1;
    public string? StorageLocation { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<TastingNote> TastingNotes { get; set; } = new List<TastingNote>();
    public ICollection<DrinkingWindow> DrinkingWindows { get; set; } = new List<DrinkingWindow>();
}
