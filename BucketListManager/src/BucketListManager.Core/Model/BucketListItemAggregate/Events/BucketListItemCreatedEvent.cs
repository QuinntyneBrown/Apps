// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Core;

public record BucketListItemCreatedEvent
{
    public Guid BucketListItemId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public Category Category { get; init; }
    public Priority Priority { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
