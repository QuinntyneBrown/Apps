// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core;

public class Milestone
{
    public Guid MilestoneId { get; set; }
    public Guid UserId { get; set; }
    public Guid BucketListItemId { get; set; }
    public BucketListItem? BucketListItem { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
