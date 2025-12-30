// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core;

public record WishlistItemAddedEvent
{
    public Guid WishlistId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
