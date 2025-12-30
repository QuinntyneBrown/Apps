// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Core;

public class TastingNote
{
    public Guid TastingNoteId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid WineId { get; set; }
    public Wine? Wine { get; set; }
    public DateTime TastingDate { get; set; } = DateTime.UtcNow;
    public int Rating { get; set; }
    public string? Appearance { get; set; }
    public string? Aroma { get; set; }
    public string? Taste { get; set; }
    public string? Finish { get; set; }
    public string? OverallImpression { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
