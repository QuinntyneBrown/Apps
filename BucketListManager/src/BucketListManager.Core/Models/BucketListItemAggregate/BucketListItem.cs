// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core;

public class BucketListItem
{
    public Guid BucketListItemId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Category Category { get; set; }
    public Priority Priority { get; set; }
    public ItemStatus Status { get; set; }
    public DateTime? TargetDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();
    public ICollection<Memory> Memories { get; set; } = new List<Memory>();
}
