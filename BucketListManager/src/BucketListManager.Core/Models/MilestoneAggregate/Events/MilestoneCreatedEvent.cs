// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core;

public record MilestoneCreatedEvent
{
    public Guid MilestoneId { get; init; }
    public Guid UserId { get; init; }
    public Guid BucketListItemId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
